using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Contacts.Plugins.DataStore.InMemory;
using Contacts.Maui.Views;

namespace Contacts.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        //dependency injection for the use cases and repositories
        //Singletons are created once and shared across the app
        //Transient are created each time they are requested
        //Scoped are created once per request (not used in this app)

        // Use Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions explicitly
        builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();
        ServiceCollectionServiceExtensions.AddSingleton<IViewContactsUseCase, ViewContactsUseCase>(builder.Services);
        builder.Services.AddSingleton<IViewContactUseCase, ViewContactUseCase>();
        builder.Services.AddTransient<IEditContactUseCase, EditContactUseCase>();

        // we have to inject the contacts page into the program because it doesn't have a default constructor
        // (it has a constructor that takes a parameter of type IViewContactsUseCase)
        // so we have to create an instance of the contacts page and pass the IViewContactsUseCase to it
        builder.Services.AddSingleton<ContactsPage>();
        builder.Services.AddSingleton<EditContactPage>();

        return builder.Build();
    }
}