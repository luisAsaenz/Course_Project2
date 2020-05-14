using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Data
{
    public class SumOfTask
    {
        [Key]
        public int Id { get; set; }
        public int NumberOfTask { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string UserId { get; set; }
        [ForeignKey("TaskTypeId")]
        public TaskType TaskType {get;set;}

        public int TastTypeId { get; set; }
    }
}
