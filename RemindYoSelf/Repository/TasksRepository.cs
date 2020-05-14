using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Repository
{
    public class TasksRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Data.UserTasks entity)
        {
            _context.Task.Add(entity);
            return Save();
        }

        public bool Delete(Data.UserTasks entity)
        {
            _context.Task.Remove(entity);
            return Save();
        }

        public ICollection<Data.UserTasks> FindAll()
        {
            return _context.Task.ToList();
        }

        public Data.UserTasks FindById(int id)
        {
            var Idfound = _context.Task.Find(id);
            return Idfound;
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(Data.UserTasks entity)
        {
            _context.Task.Update(entity);
            return Save();
        }
    }
}
