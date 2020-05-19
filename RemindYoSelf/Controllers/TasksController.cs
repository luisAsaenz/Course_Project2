using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            var userstasks = _repoTask.FindAll().ToList();
            var tasktype = _repo.FindAll().ToList();
            IEnumerable<TaskViewModel> t = userstasks.Join(tasktype, task => task.TaskTypeId, type => type.Id, (task, type) => new TaskViewModel
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                Tasks = task.Tasks,
                TaskDue = task.TaskDue,
                TaskTypeName = type.Name
            }).OrderBy(x => x.TaskDue);
            int num = t.Count();

            var model = _mapper.Map<List<UserTasks>, List<TaskViewModel>>(userstasks);
            return View(t);

        }
        public ActionResult TodayTask()
        {
            var userstasks = _repoTask.FindAll().ToList();
            var tasktype = _repo.FindAll().ToList();
            DateTime d = DateTime.Now.Date;

            
            IEnumerable<TaskViewModel> T = from ut in userstasks join tt in tasktype on ut.TaskTypeId equals tt.Id where ut.TaskDue == DateTime.Now.Date select new TaskViewModel{
                TaskId = ut.TaskId,
                TaskTitle = ut.TaskTitle,
                Tasks = ut.Tasks,
                TaskDue = ut.TaskDue,
                TaskTypeName = tt.Name
            } ;
            //IEnumerable < TaskViewModel > t = userstasks.Join(tasktype, task => task.TaskTypeId, type => type.Id, (task, type) => new TaskViewModel
            //{
            //    TaskId = task.TaskId,
            //    TaskTitle = task.TaskTitle,
            //    Tasks = task.Tasks,
            //    TaskDue = task.TaskDue,
            //    TaskTypeName = type.Name
            //}).Where(x => x.TaskDue == d);
            
            return View(T);
        }
        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            if (!_repoTask.IsExist(id))
            {
                return NotFound();
            }
            var task = _repoTask.FindById(id);
            var model = _mapper.Map<CreateTaskViewModel>(task);
            return View(model);
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
                var sot = _repoSumTask.FindAll();

                var sumoft = sot.Where(x => x.TaskTypeId == ct.TaskTypeId && x.UserId == ct.UserId).Select(x => x.Id).FirstOrDefault();
                if (!_repoSumTask.IsExist(sumoft)) 
                {
                     SumOfTasksViewModel s = new SumOfTasksViewModel()
                     {
                         NumberOfTask = 1,
                         TaskTypeId = ct.TaskTypeId,
                         UserId = ct.UserId
                     };
                     var model1 = _mapper.Map<SumOfTask>(s);
                     _context.Add(model1);
                     _context.SaveChanges();
                }
                else
                {
                    var SumT = _context.SumOfTasks.FirstOrDefault(x => x.Id.Equals(sumoft));
                    SumT.NumberOfTask++;
                    SumT.TaskTypeId = ct.TaskTypeId;
                    SumT.UserId = ct.UserId;
                    var isAgeModified = _context.Entry(SumT).Property("NumberOfTask").IsModified;
                    var isNameModified = _context.Entry(SumT).Property("TaskTypeId").IsModified;
                    var isIsRegularStudentModified = _context.Entry(SumT).Property("UserId").IsModified;

                    _context.SaveChanges();
                }

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
            
            if (!_repoTask.IsExist(id))
            {
                return NotFound();
            }
            var task = _repoTask.FindById(id);
            
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
            var task = _repoTask.FindById(id);
            if (task == null)
            {
                return NotFound();
            }
            var isDeleted = _repoTask.Delete(task);
            if (!isDeleted)
            {
                return BadRequest();
            }
           
            
            var sot = _repoSumTask.FindAll();
            var sumoft = sot.Where(x => x.TaskTypeId == task.TaskTypeId && x.UserId == task.UserId).Select(x => x.Id).FirstOrDefault();

            var SumT = _context.SumOfTasks.FirstOrDefault(x => x.Id.Equals(sumoft));
            SumT.NumberOfTask -= 1;
            SumT.TaskTypeId = task.TaskTypeId;
            SumT.UserId = task.UserId;
            // check in debug if property has been deleted
            var isAgeModified = _context.Entry(SumT).Property("NumberOfTask").IsModified;
            var isNameModified = _context.Entry(SumT).Property("TaskTypeId").IsModified;
            var isIsRegularStudentModified = _context.Entry(SumT).Property("UserId").IsModified;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CreateTaskViewModel ct, string id2)
        {
            try
            {
                // TODO: Add delete logic here
                var task = _repoTask.FindById(id);
                if (task == null)
                {
                    return NotFound();
                }
                var isDeleted = _repoTask.Delete(task);
                if (!isDeleted)
                {
                    return View(ct);
                }
                var sot = _repoSumTask.FindAll();
                var sumoft = sot.Where(x => x.TaskTypeId == ct.TaskTypeId && x.UserId == ct.UserId).Select(x => x.Id).FirstOrDefault();

                var SumT = _context.SumOfTasks.FirstOrDefault(x => x.Id.Equals(sumoft));
                SumT.NumberOfTask -= 1;
                SumT.TaskTypeId = ct.TaskTypeId;
                SumT.UserId = ct.UserId;
                // check in debug if property has been deleted
                var isAgeModified = _context.Entry(SumT).Property("NumberOfTask").IsModified;
                var isNameModified = _context.Entry(SumT).Property("TaskTypeId").IsModified;
                var isIsRegularStudentModified = _context.Entry(SumT).Property("UserId").IsModified;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(ct);
            }
        }
    }
}