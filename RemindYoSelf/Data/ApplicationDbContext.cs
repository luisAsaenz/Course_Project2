﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RemindYoSelf.Models;

namespace RemindYoSelf.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UsersInfos> UserInfo { get; set; }
        public DbSet<UserTasks> Task { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<SumOfTask> SumOfTasks { get; set; }
        public DbSet<RemindYoSelf.Models.TaskViewModel> TaskViewModel { get; set; }
        public DbSet<RemindYoSelf.Models.SumAndTaskNameViewModel> SumAndTaskNameViewModel { get; set; }
    }
}
