using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using RemindYoSelf.Models;

namespace RemindYoSelf.Controllers
{
    public class TypesAndSumsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ITaskTypeRepository _repo;
        private readonly IUserTaskRepository _repoTask;
        private readonly ISumOfTaskRepository _repoSumTask;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public TypesAndSumsController(ITaskTypeRepository repo, IMapper mapper, IUserTaskRepository repoTask, ISumOfTaskRepository repoSumTask, ApplicationDbContext context, SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _repoTask = repoTask;
            _context = context;
            _repoSumTask = repoSumTask;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        // GET: TypesAndSums
        [Authorize]
        public async Task<ActionResult> Index()
        {
            string userid = await theUser();

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
        }
        [Authorize]
        public async Task<ActionResult> HouseWork(int id)
        {
            string userid = await theUser();
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
            return View(t);
        }
        [Authorize]
        public async Task<ActionResult> Appointments( int id)
        {
            string userid = await theUser();
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
            return View(t);
        }
        [Authorize]
        public async Task<ActionResult> SocialGatherings(int id)
        {
            string userid = await theUser();
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
            return View(t);
        }
        [Authorize]
        public async Task<ActionResult> Work(int id)
        {
            string userid = await theUser();
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
            return View(t);
        }
        [Authorize]
        public async Task<ActionResult> School(int id)
        {
            string userid = await theUser();
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
            return View(t);
        }
        [Authorize]
        public async Task<ActionResult> Other(int id)
        {
            string userid = await theUser();
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

            return View(t);
        }

       
        public async Task<string> theUser()
        {
            var current_user = await _userManager.GetUserAsync(HttpContext.User);
            var userid = await _userManager.GetUserIdAsync(current_user);
            return userid;
        }

    }
}