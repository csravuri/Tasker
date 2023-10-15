using SQLite;

namespace Tasker.Models
{
	public class TaskHeaderModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.Now;
		public string ImagePath { get; set; }
		public string Status { get; set; }
	}
}