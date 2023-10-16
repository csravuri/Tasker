using Microsoft.Extensions.Logging;
using Tasker.Database;
using Tasker.ViewModels;
using Tasker.Views;

namespace Tasker;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<TaskHomeViewModel>();
		builder.Services.AddSingleton<TaskHome>();
		builder.Services.AddSingleton<TaskCreateViewModel>();
		builder.Services.AddSingleton<TaskCreate>();
		builder.Services.AddSingleton<TaskViewViewModel>();
		builder.Services.AddSingleton<TaskView>();

		builder.Services.AddSingleton<DbConnection>();

		return builder.Build();
	}
}
