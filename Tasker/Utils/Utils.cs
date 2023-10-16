namespace Tasker.Utils
{
	public class Utils
	{
		public class TaskStatus
		{
			public const string ReadyToStart = "ReadyToStart";
			public const string Running = "Running";
			public const string Paused = "Paused";
			public const string Completed = "Completed";
			public const string Cancelled = "Cancelled";
		}

		public class TaskStatusColor
		{
			public static readonly Color ReadyToStart = Color.FromArgb("#81b1f0");
			public static readonly Color Running = Color.FromArgb("#aee69a");
			public static readonly Color Paused = Color.FromArgb("#f5d8a2");
			public static readonly Color Completed = Color.FromArgb("#fff");
			public static readonly Color Cancelled = Color.FromArgb("#f5a2a2");
		}
	}
}