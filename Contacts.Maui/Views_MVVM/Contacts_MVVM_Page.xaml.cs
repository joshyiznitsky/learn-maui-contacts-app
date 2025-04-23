using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

public partial class Contacts_MVVM_Page : ContentPage
{
    private readonly ContactsViewModel contactsViewModel;
    public Contacts_MVVM_Page(ContactsViewModel contactsViewModel)
    {
        InitializeComponent();
        this.contactsViewModel = contactsViewModel;
        this.BindingContext = contactsViewModel;
    }

    override protected async void OnAppearing()
    {
        base.OnAppearing();

        // Debug binding context
        System.Diagnostics.Debug.WriteLine($"BindingContext Type: {BindingContext?.GetType().Name}");
        System.Diagnostics.Debug.WriteLine($"Contacts Count: {(BindingContext as ContactsViewModel)?.Contacts.Count}");

        await this.contactsViewModel.LoadContactsAsync();

        // Debug after loading
        System.Diagnostics.Debug.WriteLine($"Contacts Count After Load: {contactsViewModel.Contacts.Count}");
        // Debug if contacts are loaded
        if (contactsViewModel.Contacts.Count > 0)
        {
            System.Diagnostics.Debug.WriteLine($"First Contact Name: {contactsViewModel.Contacts[0].Name}");
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("No contacts loaded.");
        }
    }
}