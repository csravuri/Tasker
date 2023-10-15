using SQLite;
using System;

namespace Tasker.Models
{
    public class TaskLineModel : BaseModel
    {
        public Guid ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}