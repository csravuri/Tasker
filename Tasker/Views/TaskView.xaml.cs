using Tasker.ViewModels;

namespace Tasker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskView : ContentPage
	{
		public TaskView(TaskViewViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}

		//protected override void OnAppearing()
		//{
		//    base.OnAppearing();
		//    viewModel.OnAppearing();
		//}
	}
}