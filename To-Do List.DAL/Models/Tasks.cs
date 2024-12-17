using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.DAL.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Pending"; 
        public string Priority { get; set; } = "Low"; 
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
    }

}
