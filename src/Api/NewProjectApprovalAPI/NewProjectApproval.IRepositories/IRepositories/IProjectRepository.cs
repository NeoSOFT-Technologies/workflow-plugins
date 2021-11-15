using NewProjectApproval.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectApproval.IRepositories.IRepositories
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
    }
}
