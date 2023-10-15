using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Tasker.Models;
using Tasker.Views;
using Xamarin.Forms;
using TaskStatus = Tasker.Utils.Utils.TaskStatus;

namespace Tasker.ViewModels
{
    public class TaskHomeViewModel : BaseViewModel
    {
        public ICommand NewTaskCreateCommand => new Command(async () => await Shell.Current.GoToAsync(nameof(TaskCreate)));

        public ObservableCollection<TaskHeaderModel> Running { get; private set; }
        public ObservableCollection<TaskHeaderModel> Paused { get; private set; }
        public ObservableCollection<TaskHeaderModel> ReadyToStart { get; private set; }

        public TaskHomeViewModel()
        {
            Running = new ObservableCollection<TaskHeaderModel>();
            Paused = new ObservableCollection<TaskHeaderModel>();
            ReadyToStart = new ObservableCollection<TaskHeaderModel>();
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await ReLoadTasks();
        }

        private async Task ReLoadTasks()
        {
            var dbConnection = await GetDbConnection();

            var taskHeaders = await dbConnection.TaskHeaders;
            LoadTasks(Running, taskHeaders.Where(t => t.Status == TaskStatus.Running));
            LoadTasks(Paused, taskHeaders.Where(t => t.Status == TaskStatus.Paused));
            LoadTasks(ReadyToStart, taskHeaders.Where(t => t.Status == TaskStatus.ReadyToStart));
        }

        private void LoadTasks(ObservableCollection<TaskHeaderModel> destination, IEnumerable<TaskHeaderModel> source)
        {
            destination.Clear();
            foreach (var header in source)
            {
                destination.Add(header);
            }
        }

        public async void OnTaskStatusChange(TaskHeaderModel header)
        {
            var dbConnection = await GetDbConnection();
            await dbConnection.UpdateRecord(header);
            await ReLoadTasks();
        }
    }
}