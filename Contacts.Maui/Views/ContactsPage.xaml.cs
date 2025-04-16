using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;
using System.Collections.ObjectModel;
using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views
{
    public partial class ContactsPage : ContentPage
    {
        private readonly IViewContactsUseCase viewContactsUseCase;
        public ContactsPage(IViewContactsUseCase viewContactsUseCase)
        {
            InitializeComponent();
            this.viewContactsUseCase = viewContactsUseCase;
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

        private void DeleteContact_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var contact = (Contact)menuItem.CommandParameter;
            ContactRepository.DeleteContact(contact.ContactId);
            LoadContacts();
        }

        private async void LoadContacts()
        {
            var contacts = new ObservableCollection<CoreBusiness.Contact>( await this.viewContactsUseCase.ExecuteAsync(string.Empty));
            ListContacts.ItemsSource = contacts;
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var contacts = new ObservableCollection<CoreBusiness.Contact>( await this.viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
            ListContacts.ItemsSource = contacts;
        }
    }
}