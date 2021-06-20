
using Application.Task.Commands.ViewModel;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Task.Commands
{
    public interface ITaskCommandService : ICommandService
    {     
        Task<string> Create(string companyId, CreateTaskVm vm);
        Task<string> Update(string companyId, UpdateTaskVm vm);
        Task<string> Activate(string companyId, string projectId, string id);
        Task<string> Inactivate(string companyId, string projectId, string id);
        Task<string> Complete(string companyId, string projectId, string id);
    }

}
