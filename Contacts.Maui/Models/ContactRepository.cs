using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>
        {
            new Contact { ContactId = 0, Name ="John Doe", Email = "JohnDoe@email.com"},
            new Contact { ContactId = 1, Name = "Jane Doe", Email = "JaneDoe@email.com"},
            new Contact { ContactId = 2, Name = "John Smith", Email = "JohnSmith@email.com"},
            new Contact { ContactId = 3, Name = "Jane Smith", Email = "JaneSmith@email.com:"}
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int Id)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == Id);
            if (contact != null)
            {
                return new Contact
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address
                };
            }

            return null;
        }

        public static void UpdateContact(int Id, Contact contact)
        {
            //check that contact ID passed in matches the ID of the Contact
            if (contact.ContactId != Id) return;

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }
        }

        public static void AddContact(Contact contact)
        {
            var maxId = _contacts.Max(c => c.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int Id)
        {
            var contactToDelete = _contacts.FirstOrDefault(c => c.ContactId == Id);
            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
            }
        }

        public static List<Contact> SearchContacts(string searchText)
        {
            var contactsFound = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Name) && c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            
            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Email) && c.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Phone) && c.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (contactsFound == null || contactsFound.Count == 0)
            {
                contactsFound = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Address) && c.Address.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return contactsFound;
        }
    }
}