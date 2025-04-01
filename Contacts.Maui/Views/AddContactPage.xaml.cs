using System;
using Microsoft.Maui.Controls;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views
{
    public partial class AddContactPage : ContentPage
    {
        private Contact contact;
        public AddContactPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            contact = new Contact
            {
                Name = ContactControl.Name,
                Email = ContactControl.Email,
                Phone = ContactControl.Phone,
                Address = ContactControl.Address
            };
            ContactRepository.AddContact(contact);

            Shell.Current.GoToAsync("..");
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