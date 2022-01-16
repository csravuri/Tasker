using Tasker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskView : ContentPage
    {
        private TaskViewViewModel viewModel;

        public TaskView()
        {
            InitializeComponent();
            BindingContext = viewModel = new TaskViewViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}