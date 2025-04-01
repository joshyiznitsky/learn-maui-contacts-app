using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
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
        builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();

        return builder.Build();
    }
}