namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactUseCase
    {
        //recommended: one public method per use case
        Task<CoreBusiness.Contact> ExecuteAsync(int contactId);
    }
}