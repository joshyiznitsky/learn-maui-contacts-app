using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ContactInMemoryRepository : IContactRepository
    {
        public static List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
            _contacts = new List<Contact>()
            {
                new Contact { ContactId = 0, Name = "John Doe", Email = "JohnDoe@email.com" },
                new Contact { ContactId = 1, Name = "Jane Doe", Email = "JaneDoe@email.com" },
                new Contact { ContactId = 2, Name = "John Smith", Email = "JohnSmith@email.com" },
                new Contact { ContactId = 3, Name = "Jane Smith", Email = "JaneSmith@email.com" }
            };
        }

        public Task<List<Contact>> GetContactsAsync(string searchText)
        {
            var contactsFound = _contacts
                .Where(c => !string.IsNullOrWhiteSpace(c.Name) && c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts
                    .Where(c => !string.IsNullOrWhiteSpace(c.Email) && c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts
                    .Where(c => !string.IsNullOrWhiteSpace(c.Phone) && c.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts
                    .Where(c => !string.IsNullOrWhiteSpace(c.Address) && c.Address.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Task.FromResult(contactsFound);
        }
    }
}