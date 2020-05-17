using AutoMapper;
using RemindYoSelf.Data;
using RemindYoSelf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<SumOfTask, SumOfTasksViewModel>().ReverseMap();
            CreateMap<TaskType, TaskTypeViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserTasks, TaskViewModel>().ReverseMap();
            CreateMap<UserTasks, CreateTaskViewModel>().ReverseMap();
            
        }
    }
}
 