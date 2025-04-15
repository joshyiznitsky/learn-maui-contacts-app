using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactsUseCase
    {
        //recommended: one public method per use case
        Task<List<CoreBusiness.Contact>> ExecuteAsync(string searchText);
    }
}