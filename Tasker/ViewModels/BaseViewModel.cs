using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tasker.Database;

namespace Tasker.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private bool isBusy = false;

		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		private string title = "Tasker";

		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		protected async Task<DbConnection> GetDbConnection() => new DbConnection();

		public virtual void OnAppearing()
		{
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion INotifyPropertyChanged
	}
}