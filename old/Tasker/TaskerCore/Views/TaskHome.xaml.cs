using Tasker.Models;
using Tasker.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskHome : ContentPage
    {
        private TaskHomeViewModel viewModel;

        public TaskHome()
        {
            InitializeComponent();
            BindingContext = viewModel = new TaskHomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        private void OnTaskStatusChange(object sender, object e)
        {
            viewModel.OnTaskStatusChange(e as TaskHeaderModel);
        }
    }
}