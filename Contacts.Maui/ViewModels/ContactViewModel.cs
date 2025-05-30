
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact contact;
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;
        private readonly IAddContactUseCase addContactUseCase;
        public Contact Contact
        {
            get => contact;
            set
            {
                SetProperty(ref contact, value);
            }
        }

        [ObservableProperty]
        private bool isNameProvided;

        partial void OnIsNameProvidedChanged(bool value)
        {
            System.Diagnostics.Debug.WriteLine($"IsNameProvided changed to: {value}");
        }

        [ObservableProperty]
        private bool isEmailProvided;

        partial void OnIsEmailProvidedChanged(bool value)
        {
            System.Diagnostics.Debug.WriteLine($"IsEmailProvided changed to: {value}");
        }

        [ObservableProperty]
        private bool isEmailFormatValid;

        partial void OnIsEmailFormatValidChanged(bool value)
        {
            System.Diagnostics.Debug.WriteLine($"IsEmailFormatValid changed to: {value}");
        }



        public ContactViewModel(
            IViewContactUseCase viewContactUseCase,
            IEditContactUseCase editContactUseCase,
            IAddContactUseCase addContactUseCase)
        {
            this.Contact = new Contact();
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
            this.addContactUseCase = addContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            this.Contact = await this.viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async Task EditContact()
        {
            if (await ValidateContact())
            {
                await this.editContactUseCase.ExecuteAsync(this.contact.ContactId, this.contact);
                await BackToContacts();
            }
        }

        [RelayCommand]
        public async Task AddContact()
        {
            if (await ValidateContact())
            {
                await this.addContactUseCase.ExecuteAsync(this.contact);
                await BackToContacts();
            }
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"///{nameof(Contacts_MVVM_Page)}");
        }

        private async Task<bool> ValidateContact()
        {
            if (!this.IsNameProvided)
            {
                await Shell.Current.DisplayAlert("Error", "Name is required", "OK");
                return false;
            }
            if (!this.IsEmailProvided)
            {
                await Shell.Current.DisplayAlert("Error", "Email is required", "OK");
                return false;
            }
            if (!this.IsEmailFormatValid)
            {
                await Shell.Current.DisplayAlert("Error", "Email format is invalid", "OK");
                return false;
            }

            return true;
        }


    }
}