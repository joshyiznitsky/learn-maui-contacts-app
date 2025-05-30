using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLite
{
    public class ContactSQLiteRepository : IContactRepository
    {
        private SQLiteAsyncConnection database;

        public ContactSQLiteRepository()
        {
            this.database = new SQLiteAsyncConnection(Constants.DatabasePath);
            this.database.CreateTableAsync<Contact>();
           
        }

        public async Task AddContactAsync(Contact contact)
        {
            await this.database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await GetContactByIdAsync(contactId);
            if (contact != null && contact.ContactId == contactId)
            {
                await this.database.DeleteAsync(contact);
            }
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await this.database.Table<Contact>()
                .Where(c => c.ContactId == contactId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetContactsAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return await this.database.Table<Contact>().ToListAsync();
            }

            return await this.database.QueryAsync<Contact>(
                @"SELECT * FROM Contact WHERE 
                    Name LIKE ?  OR Email LIKE ? OR Phone LIKE ? OR Address LIKE ?",
                $"%{searchText}%", $"%{searchText}%", $"%{searchText}%", $"%{searchText}%");
        }

        public async Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId == contact.ContactId)
            {
                await this.database.UpdateAsync(contact);
            }
        }
    }
}