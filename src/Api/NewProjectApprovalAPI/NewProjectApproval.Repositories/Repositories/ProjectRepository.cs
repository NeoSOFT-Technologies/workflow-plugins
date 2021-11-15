using NewProjectApproval.Entities;
using NewProjectApproval.IRepositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectApproval.Repositories.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }
    } 
}
