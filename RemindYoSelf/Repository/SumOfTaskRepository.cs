using RemindYoSelf.Contracts;
using RemindYoSelf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Repository
{
    public class SumOfTaskRepository : ISumOfTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public SumOfTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Data.SumOfTask entity)
        {
            _context.SumOfTasks.Add(entity);
            return Save();
        }

        public bool Delete(Data.SumOfTask entity)
        {
            _context.SumOfTasks.Remove(entity);
            return Save();
        }

        public ICollection<Data.SumOfTask> FindAll()
        {
            return _context.SumOfTasks.ToList();
        }

        public Data.SumOfTask FindById(int id)
        {
            var Idfound = _context.SumOfTasks.Find(id);
            return Idfound;
        }

        public bool IsExist(int id)
        {
            var exists = _context.SumOfTasks.Any(x => x.Id == id);
            return exists;
        }


        public bool Save()
        {
            var changes = _context.SaveChanges();
            return changes > 0;
        }

        public bool Update(SumOfTask entity)
        {
            _context.SumOfTasks.Update(entity);
            return Save();
        }
    }
}
