using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;
using System.Collections.ObjectModel;

namespace Contacts.Maui.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBar.Text = string.Empty;
            LoadContacts();
        }

        private async void ListContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (ListContacts.SelectedItem == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(EditContactsPage)}?Id={((Contact)ListContacts.SelectedItem).ContactId}");
        }

        private void ListContacts_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListContacts.SelectedItem= null;
        }

        private void AddContactButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AddContactPage));
        }

        private void DeleteContact_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contact = (Contact)menuItem.CommandParameter;
            ContactRepository.DeleteContact(contact.ContactId);
            LoadContacts();
        }

        private void LoadContacts()
        {
            var contacts = new ObservableCollection<Contact>(ContactRepository.GetContacts());
            ListContacts.ItemsSource = contacts;
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var contactsFound = new ObservableCollection<Contact>(ContactRepository.SearchContacts(((SearchBar)sender).Text));
            ListContacts.ItemsSource = contactsFound;
        }
    }
}