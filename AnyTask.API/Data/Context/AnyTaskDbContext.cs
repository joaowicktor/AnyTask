using AnyTask.API.Data.Entities;
using AnyTask.API.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyTask.API.Data.Context
{
    public class AnyTaskDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public AnyTaskDbContext(DbContextOptions options) : base(options) { }

        public AnyTaskDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new TaskMap());
        }
    }
}
