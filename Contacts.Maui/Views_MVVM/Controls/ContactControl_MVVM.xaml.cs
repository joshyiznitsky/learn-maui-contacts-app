using System.Runtime.CompilerServices;

namespace Contacts.Maui.Views_MVVM.Controls
{
    public partial class ContactControl_MVVM : ContentView
    {
        public bool IsForEdit { get; set; } = false;
        public bool IsForAdd { get; set; } = false;
        public ContactControl_MVVM()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsForAdd && !IsForEdit)
            {
                SaveButton.SetBinding(Button.CommandProperty, "AddContactCommand");
            }
            else if (!IsForAdd && IsForEdit)
            {
                SaveButton.SetBinding(Button.CommandProperty, "EditContactCommand");
            }
            
        }
    }
}