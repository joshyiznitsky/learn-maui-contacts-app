using Contacts.Maui.Models;
using Microsoft.Maui.Controls;
using Contacts.UseCases.Interfaces;
using Contacts.CoreBusiness;
using System.Threading.Tasks;



namespace Contacts.Maui.Views
{
    [QueryProperty(nameof(ContactId), "Id")]
    public partial class EditContactPage : ContentPage
    {
        private CoreBusiness.Contact contact;
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;
        public EditContactPage(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
        {
            InitializeComponent();
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
        }
        

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        public string ContactId
        {
            set
            {
                contact = viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
                if (contact != null)
                {
                    ContactControl.Name = contact.Name;
                    ContactControl.Email = contact.Email;
                    ContactControl.Phone = contact.Phone;
                    ContactControl.Address = contact.Address;
                }
            }
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {

            contact.Name = ContactControl.Name;
            contact.Email = ContactControl.Email;
            contact.Phone = ContactControl.Phone;
            contact.Address = ContactControl.Address;

            //ContactRepository.UpdateContact(contact.ContactId, contact);
            await editContactUseCase.ExecuteAsync(contact.ContactId, contact);
            await Shell.Current.GoToAsync("..");
        }

        private void ContactControl_OnError(object sender, string e)
        {
            DisplayAlert("Error", e, "OK");
        }
    } 
}