using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Models
{
    public class SumOfTasksViewModel
    {
        //User will create a fix number of type model depending on tasktype name and throughout the application model will be updated. Per tasktype name there will be a number of task
        //ex. UserId: 1, TaskTypeName: Chores, NumberOfTask: 10
        //    UserId: 1, TaskTypeName: Appointments, NumberOfTask: 5
        //    UserId: 1, TaskTypeName: Social Gathering, NumberOfTask: 6
        //    UserId: 2, TaskTypeName: Appointments, NumberOfTask: 3
        //    UserId: 3, TaskTypeName: Social Gatherings, NumberOfTask: 8
        public int Id { get; set; }
        public int NumberOfTask { get; set; }
        public UserViewModel User { get; set; }
        public string UserId { get; set; }
        
        public TaskTypeViewModel TaskType { get; set; }

        public int TaskTypeId { get; set; }
    }
}
