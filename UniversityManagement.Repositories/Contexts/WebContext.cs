using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Repositories.Poco;
using UniversityManagement.Utilities;

namespace UniversityManagement.Repositories.Contexts
{
    public class WebContext:DbContext
    {
        public WebContext(DbContextOptions<WebContext> options):base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Student>().HasOne(s => s.User).WithOne().HasForeignKey<Student>(s => s.UserId);
            modelBuilder.Entity<Teacher>().HasOne(t => t.User).WithOne().HasForeignKey<Teacher>(t => t.UserId);
        }
    }
}
