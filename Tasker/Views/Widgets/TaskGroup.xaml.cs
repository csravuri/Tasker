using System;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace Tasker.Views.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskGroup : ContentView
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(object), typeof(TaskGroup));
        public static readonly BindableProperty IsExpandProperty = BindableProperty.Create(nameof(IsExpand), typeof(bool), typeof(TaskGroup), propertyChanged: IsExpandChanged);
        public static readonly BindableProperty ExpandCollapseButtonIconProperty = BindableProperty.Create(nameof(ExpandCollapseButtonIcon), typeof(string), typeof(TaskGroup));
        public static readonly BindableProperty GroupTitleProperty = BindableProperty.Create(nameof(GroupTitle), typeof(string), typeof(TaskGroup));
        public static readonly BindableProperty IsItemsVisibleProperty = BindableProperty.Create(nameof(IsItemsVisible), typeof(bool), typeof(TaskGroup));
        public static readonly BindableProperty ItemsHeightProperty = BindableProperty.Create(nameof(ItemsHeight), typeof(double), typeof(TaskGroup));

        public object Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public bool IsExpand
        {
            get => (bool)GetValue(IsExpandProperty);
            set
            {
                SetValue(IsExpandProperty, value);
                IsExpandChanged(value);
            }
        }

        public string ExpandCollapseButtonIcon
        {
            get => (string)GetValue(ExpandCollapseButtonIconProperty);
            set => SetValue(ExpandCollapseButtonIconProperty, value);
        }

        public string GroupTitle
        {
            get => (string)GetValue(GroupTitleProperty);
            set => SetValue(GroupTitleProperty, value);
        }

        public bool IsItemsVisible
        {
            get => (bool)GetValue(IsItemsVisibleProperty);
            set => SetValue(IsItemsVisibleProperty, value);
        }

        public double ItemsHeight
        {
            get => (double)GetValue(ItemsHeightProperty);
            set => SetValue(ItemsHeightProperty, value);
        }

        public event EventHandler<object> TaskStatusChange;

        public TaskGroup()
        {
            InitializeComponent();
            IsExpand = false;
        }

        private void OnTaskStatusChange(object sender, Models.TaskHeaderModel e)
        {
            TaskStatusChange?.Invoke(this, e);
        }

        private void UpdateExpandCollapseButtonIcon(bool isExpand)
        {
            ExpandCollapseButtonIcon = isExpand ? "task_collapse.png" : "task_expand.png";
        }

        private void ExpandCollapseButtonClicked(object sender, EventArgs e)
        {
            IsExpand = !IsExpand;
        }

        private static void IsExpandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var taskGroup = bindable as TaskGroup;
            taskGroup.IsExpandChanged((bool)newValue);
        }

        private void IsExpandChanged(bool isExpand)
        {
            UpdateExpandCollapseButtonIcon(isExpand);
            IsItemsVisible = isExpand;
        }
    }
}