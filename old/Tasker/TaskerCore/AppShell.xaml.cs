using Tasker.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Tasker
{
    public partial class AppShell : Microsoft.Maui.Controls.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TaskCreate), typeof(TaskCreate));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
    }
}