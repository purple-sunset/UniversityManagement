using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Repositories.Poco;

namespace UniversityManagement.Repositories.Contexts
{
    public class WebContext:DbContext
    {
        public WebContext(DbContextOptions<WebContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
