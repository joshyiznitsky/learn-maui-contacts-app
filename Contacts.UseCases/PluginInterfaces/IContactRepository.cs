using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task<List<CoreBusiness.Contact>> GetContactsAsync(string searchText);
    }
}