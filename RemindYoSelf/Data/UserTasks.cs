using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Data
{
    public class UserTasks
    {
        [Key]
        [NotNull]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Task can not be over 300 character")]
        public string Tasks { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDue { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User Users { get; set; }
        [ForeignKey("TaskTypeId")]
        public TaskType TaskType { get; set; }
        public int TaskTypeId { get; set; }
    }
}
