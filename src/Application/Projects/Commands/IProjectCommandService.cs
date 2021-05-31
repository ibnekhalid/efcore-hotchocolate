using Application.Company.Commands.ViewModel;
using Application.Projects.Commands.ViewModel;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public interface IProjectCommandService : ICommandService
    {
        Task<string> AddUsers(string companyId, string projectId, List<string> vm);
        Task<string> AddUser(string companyId, string projectId, string email);
        Task<string> Create(string companyId, CreateProjectVm vm);
        Task<string> Update(string companyId, UpdateProjectVm vm);
        Task<string> Activate(string companyId, string id);
        Task<string> Inactivate(string companyId, string id);
        Task<string> Complete(string companyId, string id);
    }

}
