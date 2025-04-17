using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Contacts.Maui.Models;
using Contact = Contacts.CoreBusiness.Contact;
using System.Collections.ObjectModel;
using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views
{
    public partial class ContactsPage : ContentPage
    {
        private readonly IViewContactsUseCase viewContactsUseCase;
        private readonly IDeleteContactUseCase deleteContactUseCase;
        public ContactsPage(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
        {
            InitializeComponent();
            this.viewContactsUseCase = viewContactsUseCase;
            this.deleteContactUseCase = deleteContactUseCase;
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

            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((CoreBusiness.Contact)ListContacts.SelectedItem).ContactId}");
        }

        private void ListContacts_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListContacts.SelectedItem= null;
        }

        private void AddContactButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AddContactPage));
        }

        private async void DeleteContact_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contact = (Contact)menuItem.CommandParameter;
            //ContactRepository.DeleteContact(contact.ContactId);
            await deleteContactUseCase.ExecuteAsync(contact.ContactId);
            LoadContacts();
        }

        private async void LoadContacts()
        {
            var contacts = new ObservableCollection<Contact>( await this.viewContactsUseCase.ExecuteAsync(string.Empty));
            ListContacts.ItemsSource = contacts;
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var contacts = new ObservableCollection<Contact>( await this.viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
            ListContacts.ItemsSource = contacts;
        }

        private void TestButton_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(TestPage1));
        }
    }
}