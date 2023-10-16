using Tasker.ViewModels;

namespace Tasker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskCreate : ContentPage
	{
		public TaskCreate(TaskCreateViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}
	}
}