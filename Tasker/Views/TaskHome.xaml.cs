using Tasker.ViewModels;

namespace Tasker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskHome : ContentPage
	{
		private readonly TaskHomeViewModel vm;

		public TaskHome(TaskHomeViewModel vm)
		{
			InitializeComponent();
			BindingContext = this.vm = vm;
		}

		private void OnTaskStatusChanged(object sender, Models.TaskHeaderModel e)
		{
			vm.TaskStatusChangeCommand.Execute(e);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			vm.ReLoadTasksCommand.Execute(null);
		}
	}
}