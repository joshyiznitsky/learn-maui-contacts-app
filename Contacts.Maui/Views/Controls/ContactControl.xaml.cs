using System;

namespace Contacts.Maui.Views.Controls
{
    public partial class ContactControl : ContentView
    {
        //event handlers for the button validation
        public event EventHandler<string> OnError;
        public event EventHandler<EventArgs> OnSave;
        public event EventHandler<EventArgs> OnCancel;
        public ContactControl()
        {
            InitializeComponent();
        }

        //refers to the name of the contact
        public string Name {
            get { return entryName.Text;}
            set { entryName.Text = value; }
        }

        public string Email {
            get { return entryEmail.Text; }
            set { entryEmail.Text = value; }
        }

        public string Phone {
            get { return entryPhone.Text; }
            set { entryPhone.Text = value; }
        }

        public string Address {
            get { return entryAddress.Text; }
            set { entryAddress.Text = value; }
        }

        //button handlers
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (NameValidation.IsNotValid)
            {
                OnError?.Invoke(sender, "Name is required.");
                return;
            }

            if (EmailValidaton.IsNotValid)
            {
                foreach (var error in EmailValidaton.Errors)
                {
                    OnError?.Invoke(sender, error.ToString());
                }

                return;
            }

            OnSave?.Invoke(sender, e);
        }   

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            OnCancel?.Invoke(sender, e);
        }
    }
}

