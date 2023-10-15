using Tasker.ViewModels;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCreate : ContentPage
    {
        private TaskCreateViewModel viewModel;

        public TaskCreate()
        {
            InitializeComponent();
            BindingContext = viewModel = new TaskCreateViewModel();
        }
    }
}