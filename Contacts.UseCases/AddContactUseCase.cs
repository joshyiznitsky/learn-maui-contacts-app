using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;
using Contacts.UseCases.Interfaces;

namespace Contacts.UseCases
{
    public class AddContactUseCase : IAddContactUseCase
    {
        private readonly IContactRepository contactRepository;
        public AddContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task ExecuteAsync(Contact contact)
        {
            await this.contactRepository.AddContactAsync(contact);
        }
    }
}