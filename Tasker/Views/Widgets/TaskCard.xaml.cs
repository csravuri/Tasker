using Tasker.Models;
using static Tasker.Utils.Utils;
using TaskStatus = Tasker.Utils.Utils.TaskStatus;

namespace Tasker.Views.Widgets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskCard : ContentView
	{
		public static readonly BindableProperty TaskHeaderProperty = BindableProperty.Create(nameof(TaskHeader), typeof(TaskHeaderModel), typeof(TaskCard), propertyChanged: HeaderChanged);
		public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(TaskCard));
		public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(TaskCard));
		public static readonly BindableProperty IsTaskStartedVisibleProperty = BindableProperty.Create(nameof(IsTaskStartedVisible), typeof(bool), typeof(TaskCard));
		public static readonly BindableProperty IsTaskPausedVisibleProperty = BindableProperty.Create(nameof(IsTaskPausedVisible), typeof(bool), typeof(TaskCard));
		public static readonly BindableProperty IsTaskDoneVisibleProperty = BindableProperty.Create(nameof(IsTaskDoneVisible), typeof(bool), typeof(TaskCard));
		public static readonly BindableProperty IsTaskCancelVisibleProperty = BindableProperty.Create(nameof(IsTaskCancelVisible), typeof(bool), typeof(TaskCard));
		public static readonly BindableProperty CardBackgroundColorProperty = BindableProperty.Create(nameof(CardBackgroundColor), typeof(string), typeof(TaskCard));
		public static readonly BindableProperty TaskStatusChangeCommandProperty = BindableProperty.Create(nameof(TaskStatusChangeCommand), typeof(Command<TaskHeaderModel>), typeof(TaskCard));

		public TaskHeaderModel TaskHeader
		{
			get => (TaskHeaderModel)GetValue(TaskHeaderProperty);
			set => SetValue(TaskHeaderProperty, value);
		}

		public string Name
		{
			get => (string)GetValue(NameProperty);
			set => SetValue(NameProperty, value);
		}

		public string Description
		{
			get => (string)GetValue(DescriptionProperty);
			set => SetValue(DescriptionProperty, value);
		}

		public bool IsTaskStartedVisible
		{
			get => (bool)GetValue(IsTaskStartedVisibleProperty);
			set => SetValue(IsTaskStartedVisibleProperty, value);
		}

		public bool IsTaskPausedVisible
		{
			get => (bool)GetValue(IsTaskPausedVisibleProperty);
			set => SetValue(IsTaskPausedVisibleProperty, value);
		}

		public bool IsTaskDoneVisible
		{
			get => (bool)GetValue(IsTaskDoneVisibleProperty);
			set => SetValue(IsTaskDoneVisibleProperty, value);
		}

		public bool IsTaskCancelVisible
		{
			get => (bool)GetValue(IsTaskCancelVisibleProperty);
			set => SetValue(IsTaskCancelVisibleProperty, value);
		}

		public string CardBackgroundColor
		{
			get => (string)GetValue(CardBackgroundColorProperty);
			set => SetValue(CardBackgroundColorProperty, value);
		}

		public event EventHandler<TaskHeaderModel> TaskStatusChange;

		public Command<TaskHeaderModel> TaskStatusChangeCommand { get; set; }

		public TaskCard()
		{
			InitializeComponent();
		}

		public void LoadData(TaskHeaderModel header)
		{
			Name = header.Name;
			Description = header.Description;
			DecideButtonVisibility(header);
		}

		private void DecideButtonVisibility(TaskHeaderModel header)
		{
			switch (header.Status)
			{
				case TaskStatus.ReadyToStart:
					IsTaskStartedVisible = true;
					IsTaskPausedVisible = false;
					IsTaskDoneVisible = false;
					IsTaskCancelVisible = true;
					CardBackgroundColor = TaskStatusColor.ReadyToStart;
					break;

				case TaskStatus.Running:
					IsTaskStartedVisible = false;
					IsTaskPausedVisible = true;
					IsTaskDoneVisible = true;
					IsTaskCancelVisible = false;
					CardBackgroundColor = TaskStatusColor.Running;
					break;

				case TaskStatus.Paused:
					IsTaskStartedVisible = true;
					IsTaskPausedVisible = false;
					IsTaskDoneVisible = true;
					IsTaskCancelVisible = false;
					CardBackgroundColor = TaskStatusColor.Paused;
					break;

				default:
					IsTaskStartedVisible = false;
					IsTaskPausedVisible = false;
					IsTaskDoneVisible = false;
					IsTaskCancelVisible = false;
					CardBackgroundColor = TaskStatusColor.Completed;
					break;
			}
		}

		private void TaskStartClicked(object sender, EventArgs e)
		{
			TaskHeader.Status = TaskStatus.Running;
			InformTaskStatusChange();
		}

		private void TaskPauseClicked(object sender, EventArgs e)
		{
			TaskHeader.Status = TaskStatus.Paused;
			InformTaskStatusChange();
		}

		private void TaskDoneClicked(object sender, EventArgs e)
		{
			TaskHeader.Status = TaskStatus.Completed;
			InformTaskStatusChange();
		}

		private void TaskCancelClicked(object sender, EventArgs e)
		{
			TaskHeader.Status = TaskStatus.Cancelled;
			InformTaskStatusChange();
		}

		void InformTaskStatusChange()
		{
			TaskStatusChange?.Invoke(this, TaskHeader);
			TaskStatusChangeCommand?.Execute(TaskHeader);
			DecideButtonVisibility(TaskHeader);
		}

		private static void HeaderChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var taskCard = bindable as TaskCard;
			if (newValue != null)
			{
				taskCard.LoadData(newValue as TaskHeaderModel);
			}
		}
	}
}