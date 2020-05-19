using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using RemindYoSelf.Models;

namespace RemindYoSelf.Controllers
{
    public class TypesAndSumsController : Controller
    {
        private readonly ITaskTypeRepository _repo;
        private readonly IUserTaskRepository _repoTask;
        private readonly ISumOfTaskRepository _repoSumTask;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public TypesAndSumsController(ITaskTypeRepository repo, IMapper mapper, IUserTaskRepository repoTask, ISumOfTaskRepository repoSumTask, ApplicationDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _repoTask = repoTask;
            _context = context;
            _repoSumTask = repoSumTask;
        }
        // GET: TypesAndSums
        public ActionResult Index(string userid)
        {
            var usersumTask = _repoSumTask.FindAll().ToList();
            var tasktype = _repo.FindAll().ToList();
            IEnumerable<SumAndTaskNameViewModel> t = usersumTask.Join(tasktype, task => task.TaskTypeId, type => type.Id, (task, type) => new SumAndTaskNameViewModel
            {
                Id = task.Id,
                TaskTypeId = task.TaskTypeId,
                NumberOfTask = task.NumberOfTask,
                TaskName = type.Name,
                UserId = task.UserId,
                UrlRouting = type.Name.Replace(" ", "")
            }).Where(x => x.UserId == userid);
            //string j = t.Where(x => x.TaskTypeId == 3).Select(x => x.UrlRouting).FirstOrDefault();
            //string l = "SocialGatherings";
            //bool test = j == l;
            //int w = 1;
            return View(t);
        }public ActionResult HouseWork(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);
            return View(t);
        }
        public ActionResult Appointments(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);
            return View(t);
        }
        public ActionResult SocialGatherings(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);
            return View(t);
        }
        public ActionResult Work(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);
            return View(t);
        }
        public ActionResult School(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);
            return View(t);
        }
        public ActionResult Other(string userid, int id)
        {
            IEnumerable<TaskViewModel> t = TaskByTaskType(userid, id);

            return View(t);
        }

        private IEnumerable<TaskViewModel> TaskByTaskType(string userid, int id)
        {
            var userstasks = _repoTask.FindAll().ToList();
            var tasktype = _repo.FindAll().ToList();
            IEnumerable<TaskViewModel> t = userstasks.Join(tasktype, task => task.TaskTypeId, type => type.Id, (task, type) => new TaskViewModel
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                Tasks = task.Tasks,
                TaskDue = task.TaskDue,
                TaskTypeName = type.Name,
                UserId = task.UserId,
                TaskTypeId = type.Id, 
                
            }).Where(x => x.UserId == userid && x.TaskTypeId == id);
            int num = t.Count();
            return t;
        }
        
    }
}