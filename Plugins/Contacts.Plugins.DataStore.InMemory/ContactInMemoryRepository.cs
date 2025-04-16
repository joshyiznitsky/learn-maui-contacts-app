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

        public Task AddContactAsync(Contact contact)
        {
            var maxId = _contacts.Max(c => c.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);

            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int contactId)
        {
            var contactToDelete = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
            }

            return Task.CompletedTask;
        }

        public Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contact != null)
            {
                return Task.FromResult(new Contact
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address
                });
            }

            return null;
        }

        public Task<List<Contact>> GetContactsAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return Task.FromResult(_contacts);
            }
            
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

        public Task UpdateContactAsync(int contactId, Contact contact)
        {
            //check that contact ID passed in matches the ID of the Contact
            if (contact.ContactId != contactId) return Task.CompletedTask;

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }

            return Task.CompletedTask;
        }
    }
}