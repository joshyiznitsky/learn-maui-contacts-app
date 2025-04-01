using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;
using Microsoft.Maui.Controls;


namespace Contacts.Maui.Views
{
    [QueryProperty(nameof(ContactId), "Id")]
    public partial class EditContactsPage : ContentPage
    {
        private Contact contact;
        public EditContactsPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }

        public string ContactId
        {
            set
            {
                contact = ContactRepository.GetContactById(int.Parse(value));
                if (contact != null)
                {
                    ContactControl.Name = contact.Name;
                    ContactControl.Email = contact.Email;
                    ContactControl.Phone = contact.Phone;
                    ContactControl.Address = contact.Address;
                }
            }
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {

            contact.Name = ContactControl.Name;
            contact.Email = ContactControl.Email;
            contact.Phone = ContactControl.Phone;
            contact.Address = ContactControl.Address;

            ContactRepository.UpdateContact(contact.ContactId, contact);
            Shell.Current.GoToAsync("..");
        }

        private void ContactControl_OnError(object sender, string e)
        {
            DisplayAlert("Error", e, "OK");
        }
    } 
}