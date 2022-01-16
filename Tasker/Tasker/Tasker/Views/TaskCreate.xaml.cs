using Tasker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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