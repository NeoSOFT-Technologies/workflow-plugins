using NewProjectApproval.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewProjectApproval.Repositories
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }

    }
}
