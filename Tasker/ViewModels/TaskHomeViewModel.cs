using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tasker.Database;
using Tasker.Models;
using Tasker.Views;
using TaskStatus = Tasker.Utils.Utils.TaskStatus;

namespace Tasker.ViewModels
{
	public partial class TaskHomeViewModel : ObservableObject
	{
		public TaskHomeViewModel(DbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
			Title = "Home";
			Tasks = new ObservableCollection<TaskHeaderGroupModel>();
		}

		public ObservableCollection<TaskHeaderGroupModel> Tasks { get; private set; }

		[ObservableProperty]
		string title;

		[RelayCommand]
		async Task CreateNewTask()
		{
			await Shell.Current.GoToAsync(nameof(TaskCreate));
		}

		[RelayCommand]
		async Task ReLoadTasks()
		{
			var taskHeaders = await dbConnection.GetAll<TaskHeaderModel>(x => true);
			Tasks.Clear();

			LoadTasks("Running", taskHeaders.Where(t => t.Status == TaskStatus.Running));
			LoadTasks("Paused", taskHeaders.Where(t => t.Status == TaskStatus.Paused));
			LoadTasks("Ready to Start", taskHeaders.Where(t => t.Status == TaskStatus.ReadyToStart));
		}

		[RelayCommand]
		async Task TaskStatusChange(TaskHeaderModel header)
		{
			await UpdateOtherRunningTask(header);
			await dbConnection.Update(header);
			await ReLoadTasks();
		}

		async Task UpdateOtherRunningTask(TaskHeaderModel header)
		{
			if (header.Status != TaskStatus.Running)
			{
				return;
			}

			foreach (var task in await dbConnection.GetAll<TaskHeaderModel>(x => x.Id != header.Id && x.Status == TaskStatus.Running))
			{
				task.Status = TaskStatus.Paused;
				await dbConnection.Update(task);
			}
		}

		void LoadTasks(string name, IEnumerable<TaskHeaderModel> items)
		{
			if (items == null || !items.Any())
			{
				return;
			}
			Tasks.Add(new TaskHeaderGroupModel(name, items));
		}

		private readonly DbConnection dbConnection;
	}
}