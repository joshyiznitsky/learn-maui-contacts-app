using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Contacts.Plugins.DataStore.InMemory;

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
        ServiceCollectionServiceExtensions.AddSingleton<IContactRepository, ContactInMemoryRepository>(builder.Services);
        ServiceCollectionServiceExtensions.AddSingleton<IViewContactsUseCase, ViewContactsUseCase>(builder.Services);

        return builder.Build();
    }
}