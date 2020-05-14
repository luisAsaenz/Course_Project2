using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Models
{
    public class TaskTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // There will be a fixed # of TaskTyped names to choose from. For anything else the option will be other
    }
}
