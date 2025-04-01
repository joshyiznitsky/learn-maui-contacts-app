using Contacts.Maui.Views;

namespace Contacts.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
		Routing.RegisterRoute(nameof(EditContactsPage), typeof(EditContactsPage));
		Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
		
	}
}
