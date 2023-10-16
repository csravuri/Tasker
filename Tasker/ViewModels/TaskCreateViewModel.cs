using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tasker.Database;
using Tasker.Models;
using TaskStatus = Tasker.Utils.Utils.TaskStatus;

namespace Tasker.ViewModels
{
	public partial class TaskCreateViewModel : ObservableObject
	{
		public TaskCreateViewModel(DbConnection dbConnection)
		{
			Title = "Create New Task";
			this.dbConnection = dbConnection;
		}

		[ObservableProperty]
		string taskName;

		[ObservableProperty]
		private string taskDescription;

		[ObservableProperty]
		private string title;

		[ObservableProperty]
		bool stayHereOnCreate = true;

		[ObservableProperty]
		string stayHereOnCreateCaption = "Stay Here On Create";

		private readonly DbConnection dbConnection;

		[RelayCommand]
		async Task Create()
		{
			if (IsValid())
			{
				var taskHeader = new TaskHeaderModel
				{
					Name = TaskName,
					Description = TaskDescription,
					Status = TaskStatus.ReadyToStart
				};

				await dbConnection.Create(taskHeader);

				if (StayHereOnCreate)
				{
					ClearAll();
				}
				else
				{
					await Shell.Current.GoToAsync("..");
				}
			}
		}

		[RelayCommand]
		async Task Cancel()
		{
			await Shell.Current.GoToAsync("..");
		}

		[RelayCommand]
		void TapStayHereOnCreate()
		{
			StayHereOnCreate = !StayHereOnCreate;
		}

		bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(TaskName))
			{
				Shell.Current.DisplayAlert("Error", "Title required", "OK");
				return false;
			}

			return true;
		}

		void ClearAll()
		{
			TaskName = string.Empty;
			TaskDescription = string.Empty;
		}
	}
}