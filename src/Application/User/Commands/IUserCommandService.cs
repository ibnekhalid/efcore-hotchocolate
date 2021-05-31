using Application.Company.Commands.ViewModel;
using Application.Projects.Commands.ViewModel;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public interface IUserCommandService : ICommandService
    {
        Task<string> Create(string companyId, CreateUserVm vm);
        Task<string> Activate(string companyId, string id);
        Task<string> Inactivate(string companyId, string id);

    }

}
