using Tasker.Views;
using Xamarin.Forms;

namespace Tasker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TaskCreate), typeof(TaskCreate));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}