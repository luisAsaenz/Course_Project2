using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                //var sum = _repoSumTask.FindAll();
                //IEnumerable<string> user = sum.Where(x => x.UserId == ct.UserId && x.TastTypeId == ct.TaskTypeId).Select(x => x.UserId);
                //if (user.Contains(ct.UserId))
                //{
                //    //SumOfTask sumOfTask = new SumOfTask();
                //    //sumOfTask.NumberOfTask++;
                //    //sumOfTask.TastTypeId = ct.TaskTypeId;
                //    //sumOfTask.UserId = ct.UserId;
                //    //_context.Update()
                //    var SumofT = sum.Where(x => x.UserId == ct.UserId && x.TastTypeId == ct.TaskTypeId);
                //    foreach (SumOfTask t in SumofT)
                //    {
                //        t.NumberOfTask++;
                //    }
                //    _context.SaveChanges();
                //}
                
                
                //SumOfTask sumOfTask = new SumOfTask();
                //sumOfTask.NumberOfTask = 1;
                //sumOfTask.TastTypeId = ct.TaskTypeId;
                //sumOfTask.UserId = ct.UserId;
                //_context.SumOfTasks.Add(sumOfTask);
                //_context.SaveChanges();

                
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
            return View();
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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