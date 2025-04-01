using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases;

// All the code in this file is included in all platforms.
public class ViewContactsUseCase
{
    private readonly IContactRepository contactRepository;
    public ViewContactsUseCase(IContactRepository contactRepository)
    {
        this.contactRepository = contactRepository;
    }

    //recommended: one public method per use case
    public async Task<List<Contact>> ExecuteAsync(string searchText)
    {
        return await this.contactRepository.GetContactsAsync(searchText);
    }
}
