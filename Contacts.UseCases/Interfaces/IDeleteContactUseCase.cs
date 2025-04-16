namespace Contacts.UseCases.Interfaces
{
    public interface IDeleteContactUseCase
    {
        //recommended: one public method per use case
        Task ExecuteAsync(int contactId);
    }
}