using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task<List<CoreBusiness.Contact>> GetContactsAsync(string searchText);
        Task<bool> AddContactAsync(CoreBusiness.Contact contact);
        Task<bool> UpdateContactAsync(CoreBusiness.Contact contact);
        Task<bool> DeleteContactAsync(int id);
        Task<CoreBusiness.Contact> GetContactByIdAsync(int id);
    }
}