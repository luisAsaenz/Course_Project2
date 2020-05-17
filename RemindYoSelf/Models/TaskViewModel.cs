using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RemindYoSelf.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RemindYoSelf.Models
{
    public class TaskViewModel
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Your title is too long")]
        public string TaskTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Task can not be over 300 character")]
        [Required]
        public string Tasks { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDue { get; set; }

        public UserViewModel Users { get; set; }
       
        public string UserId { get; set; }
    
        public TaskTypeViewModel TaskType { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskTypeName { get; set; }
        
    }
    public class CreateTaskViewModel
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Your title is too long")]
        [Display(Name = "Task Of Title")]
        public string TaskTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Task can not be over 300 character")]
        [Required]
        public string Tasks { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Task Due")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TaskDue { get; set; }

        public UserViewModel Users { get; set; }

        public string UserId { get; set; }

        public TaskTypeViewModel TaskType { get; set; }
        [Display(Name = "Catagory Of Task")]
        public int TaskTypeId { get; set; }

        
        
        
    }
}
