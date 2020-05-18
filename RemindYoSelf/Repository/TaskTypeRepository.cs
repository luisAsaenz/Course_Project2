using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace RemindYoSelf.Repository
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Data.TaskType entity)
        {
            _context.TaskTypes.Add(entity);
            return Save();
        }

        public bool Delete(Data.TaskType entity)
        {
            _context.TaskTypes.Remove(entity);
            return Save();
        }

        public ICollection<Data.TaskType> FindAll()
        {
           return  _context.TaskTypes.ToList();
        }

        public Data.TaskType FindById(int id)
        {
            var Idfound = _context.TaskTypes.Find(id);
            return Idfound;
        }

        public bool IsExist(int id)
        {
            var exists = _context.TaskTypes.Any(x => x.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(Data.TaskType entity)
        {
            _context.TaskTypes.Update(entity);
            return Save();
        }
    }
}
