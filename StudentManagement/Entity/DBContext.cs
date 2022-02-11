using StudentManagement.Entity.Students;
using StudentManagement.Entity.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StudentManagement.Entity
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=StudentManagementDB")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<MajorSubject> MajorSubject { get; set; }
        public DbSet<Designation> Designation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
