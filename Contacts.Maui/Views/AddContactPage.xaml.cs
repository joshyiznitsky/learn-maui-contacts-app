using System;
using Microsoft.Maui.Controls;
using Contacts.Maui.Models;
using Contact = Contacts.CoreBusiness.Contact;
using Contacts.UseCases.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Contacts.Maui.Views
{
    public partial class AddContactPage : ContentPage
    {
        private Contact contact;
        private readonly IAddContactUseCase addContactUseCase;
        public AddContactPage(IAddContactUseCase addContactUseCase)
        {
            InitializeComponent();
            this.addContactUseCase = addContactUseCase;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            contact = new Contact
            {
                Name = ContactControl.Name,
                Email = ContactControl.Email,
                Phone = ContactControl.Phone,
                Address = ContactControl.Address
            };
            //ContactRepository.AddContact(contact);
            await addContactUseCase.ExecuteAsync(contact);

            await Shell.Current.GoToAsync("..");
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        private void ContactControl_OnError(object sender, string e)
        {
            DisplayAlert("Error", e, "OK");
        }
    }
}