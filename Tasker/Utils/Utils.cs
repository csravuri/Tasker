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
            public const string ReadyToStart = "#81b1f0";
            public const string Running = "#aee69a";
            public const string Paused = "#f5d8a2";
            public const string Completed = "#fff";
            public const string Cancelled = "#f5a2a2";
        }
    }
}