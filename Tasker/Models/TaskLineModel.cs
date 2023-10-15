using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace Tasker.Models
{
	public class TaskLineModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		[ForeignKey(nameof(TaskHeaderModel))]
		public int HeaderId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}