using System;

namespace Tasker.Models
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool Save()
        {
            return false;
        }

        public bool Delete()
        {
            return false;
        }
    }
}