using Life.Application;
using Life.Application.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LifeApp
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

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif

			{
				IConfiguration configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();

				builder.Services.AddSingleton(configuration);
			}

			builder.Services.AddSingleton<IFamilySummaryQueryService, FamilySummaryQueryService>();
			builder.Services.AddSingleton<IFamilyQueryService, FamilyQueryService>();
			builder.Services.AddSingleton<IHumanSummaryQueryService, HumanSummaryQueryService>();
			builder.Services.AddSingleton<IHumanQueryService, HumanQueryService>();
			builder.Services.AddSingleton<IRecipeSummaryQueryService, RecipeSummaryQueryService>();
			builder.Services.AddSingleton<IRecipeQueryService, RecipeQueryService>();
			builder.Services.AddSingleton<IItemSummaryQueryService, ItemSummaryQueryService>();

			return builder.Build();
		}
	}
}
