using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Tasker.Models;
using TaskStatus = Tasker.Utils.Utils.TaskStatus;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Tasker.ViewModels
{
    public class TaskCreateViewModel : BaseViewModel
    {
        public ICommand CreateClickedCommand => new Command(async () => await ExecuteCreateClickedCommand());

        public ICommand CancelClikedCommand => new Command(async () => await Shell.Current.GoToAsync(".."));

        public string TaskName
        {
            get => taskName;
            set => SetProperty(ref taskName, value);
        }

        private string taskName = string.Empty;

        public string TaskDescription
        {
            get => taskDescription;
            set => SetProperty(ref taskDescription, value);
        }

        private string taskDescription = string.Empty;

        public TaskCreateViewModel()
        {
            Title = "Create New Task";
        }

        private async Task ExecuteCreateClickedCommand()
        {
            if (IsValid())
            {
                var dbConnection = await GetDbConnection();
                var taskHeader = new TaskHeaderModel
                {
                    ID = Guid.NewGuid(),
                    Name = TaskName,
                    Description = TaskDescription,
                    Status = TaskStatus.ReadyToStart
                };

                await dbConnection.InsertRecord(taskHeader);

                if (await Shell.Current.DisplayAlert("Info", "Want to create again?", "Yes", "No"))
                {
                    ClearAll();
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(TaskName))
            {
                Shell.Current.DisplayAlert("Error", "Title required", "OK");
                return false;
            }

            return true;
        }

        private void ClearAll()
        {
            TaskName = string.Empty;
            TaskDescription = string.Empty;
        }
    }
}