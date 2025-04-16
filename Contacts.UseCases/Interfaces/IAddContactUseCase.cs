namespace Contacts.UseCases.Interfaces
{
    public interface IAddContactUseCase
    {
        //recommended: one public method per use case
        Task ExecuteAsync(CoreBusiness.Contact contact);
    }
}