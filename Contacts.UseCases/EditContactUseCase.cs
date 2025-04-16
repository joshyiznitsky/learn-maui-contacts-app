using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;
using Contacts.UseCases.Interfaces;

namespace Contacts.UseCases
{
    public class EditContactUseCase : IEditContactUseCase
    {
        private readonly IContactRepository contactRepository;
        public EditContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        public async Task ExecuteAsync(int contactId, Contact contact)
        {
            await this.contactRepository.UpdateContactAsync(contactId, contact);
        }
    }
}