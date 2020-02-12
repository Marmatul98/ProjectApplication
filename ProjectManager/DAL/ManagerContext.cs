using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManager.DAL
{
    public class ManagerContext : DbContext
    {
        public ManagerContext() : base("ManagerContext")
        {
        }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Year> Years { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
             .HasMany(p => p.Keywords).WithMany(i => i.Projects)
             .Map(t => t.MapLeftKey("ProjectID")
                 .MapRightKey("KeywordID")
                 .ToTable("ProjectKeyword"));
        }
    }
}