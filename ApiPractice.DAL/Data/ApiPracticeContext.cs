﻿using APiPracticeSql.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiPractice.DAL.Data
{
    public class ApiPracticeContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public ApiPracticeContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
