using Microsoft.Extensions.Logging;
using RecipeBook.Data;

namespace RecipeBook
{
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazorBootstrap();
            // Set path to the SQLite database (it will be created if it does not exist)
            var dbPath =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Recipes.db");
            // Register RecipeService and the SQLite database
            builder.Services.AddSingleton<RecipeDatabase>(
                s => ActivatorUtilities.CreateInstance<RecipeDatabase>(s, dbPath));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		    builder.Logging.AddDebug();
            #endif

            return builder.Build();
        }
    }
}
