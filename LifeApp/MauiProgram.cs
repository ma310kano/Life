using Life.Application;
using Life.Application.Sqlite;
using Life.Domain.Model;
using Life.Domain.Model.Sqlite;
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

			builder.Services.AddSingleton<IAreaSummaryQueryService, AreaSummaryQueryService>();
			builder.Services.AddSingleton<IAreaQueryService, AreaQueryService>();
			builder.Services.AddSingleton<IBuildingSummaryQueryService, BuildingSummaryQueryService>();
			builder.Services.AddSingleton<IBuildingQueryService, BuildingQueryService>();
			builder.Services.AddSingleton<IBuildingRecipeSummaryQueryService, BuildingRecipeSummaryQueryService>();
			builder.Services.AddSingleton<IBuildingRecipeQueryService, BuildingRecipeQueryService>();
			builder.Services.AddSingleton<IFamilySummaryQueryService, FamilySummaryQueryService>();
			builder.Services.AddSingleton<IFamilyQueryService, FamilyQueryService>();
			builder.Services.AddSingleton<IHumanSummaryQueryService, HumanSummaryQueryService>();
			builder.Services.AddSingleton<IHumanQueryService, HumanQueryService>();
			builder.Services.AddSingleton<IHumanAreaMovementService, HumanAreaMovementService>();
			builder.Services.AddSingleton<IHumanGatheringService, HumanGatheringService>();
			builder.Services.AddSingleton<IHumanAreaBuidingService, HumanAreaBuidingService>();
			builder.Services.AddSingleton<IHumanItemMakingService, HumanItemMakingService>();
			builder.Services.AddSingleton<IHumanContextFactory, HumanContextFactory>();
			builder.Services.AddSingleton<IItemSummaryQueryService, ItemSummaryQueryService>();
			builder.Services.AddSingleton<IItemQueryService, ItemQueryService>();
			builder.Services.AddSingleton<IItemRecipeSummaryQueryService, ItemRecipeSummaryQueryService>();
			builder.Services.AddSingleton<IItemRecipeQueryService, ItemRecipeQueryService>();

			return builder.Build();
		}
	}
}
