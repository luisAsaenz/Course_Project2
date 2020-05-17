using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindYoSelf.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNUmber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<TaskViewModel> UserTask { get; set; }
    }
}
