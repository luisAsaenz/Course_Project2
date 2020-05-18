using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Internal;
using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using RemindYoSelf.Models;

namespace RemindYoSelf.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskTypeRepository _repo;
        private readonly IUserTaskRepository _repoTask;
        private readonly ISumOfTaskRepository _repoSumTask;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public TasksController(ITaskTypeRepository repo, IMapper mapper, IUserTaskRepository repoTask, ISumOfTaskRepository repoSumTask, ApplicationDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _repoTask = repoTask;
            _context = context;
            _repoSumTask = repoSumTask;
        }
        // GET: Task
        public ActionResult Index()
        {
            var userstasks = _repoTask.FindAll();
            var tasktype = _repo.FindAll().ToList();
            IEnumerable<TaskViewModel> t = userstasks.Join(tasktype, task => task.TaskTypeId, type => type.Id, (task, type) => new TaskViewModel
            {
                TaskTitle = task.TaskTitle,
                Tasks = task.Tasks,
                TaskDue = task.TaskDue,
                TaskTypeName = type.Name
            }).OrderBy(x => x.TaskDue);
            
            
            return View(t);

        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTaskViewModel ct)
        {
            try
            {
                // TODO: Add insert logic here
                if(!ModelState.IsValid)
                {
                    return View(ct);
                }
                var tasks = _mapper.Map<UserTasks>(ct);
                var created =_repoTask.Create(tasks);
                if (!created)
                {
                    ModelState.AddModelError("", "Something Went Wrong..");
                    return View(ct);
                }
                var sot = _repo.FindAll();
                SumOfTasksViewModel s = new SumOfTasksViewModel()
                {
                    NumberOfTask = 1,
                    TaskTypeId = ct.TaskTypeId,
                    UserId = ct.UserId
                };
                var model2 = _mapper.Map<SumOfTask>(s);
                _context.Add(model2);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong..");
                return View(ct);
            }
        }

        // GET: Task/Edit/5
        // Todo: Make edit task searchable for task. Make more action methods for Edit Task
        public ActionResult Edit(int id)
        {
            if (!_repo.IsExist(id))
            {
                return NotFound();
            }
            var task = _repo.FindById(id);
            var model = _mapper.Map<CreateTaskViewModel>(task);
            return View(model);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateTaskViewModel ct)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(ct);
                }
                var task = _mapper.Map<UserTasks>(ct);
                var isUpdated = _repoTask.Update(task);
                if (!isUpdated)
                {
                    ModelState.AddModelError("", "Something went wrong updating changes");
                    return View(ct);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(ct);
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}