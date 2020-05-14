using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Models
{
    public class DetailsTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Task can not be over 300 character")]
        public string Tasks { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDue { get; set; }

        public UserViewModel Users { get; set; }
       
        public string UserId { get; set; }
    
        public TaskTypeViewModel TaskType { get; set; }
        public int TaskTypeId { get; set; }
    }
    public class CreateTaskViewModel
    {
        [Required]
        public string TaskTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Task can not be over 300 character")]
        [Required]
        public string Tasks { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public TaskTypeViewModel TaskType { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime TaskDue { get; set; }
        public IEnumerable<SelectListItem> TheTaskType { get; set; }
    }
}
