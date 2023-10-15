using SQLite;
using System;

namespace Tasker.Models
{
    public class TaskHeaderModel : BaseModel
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }
}