using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.EFCore
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
