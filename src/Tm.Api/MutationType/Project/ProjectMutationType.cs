using Application.Projects.Commands;
using Application.Projects.Commands.ViewModel;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Api.Extensions;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.MutationType.Project
{
	public class ProjectMutationType : BaseMutationType
	{
		public async Task<string> Add([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, CreateProjectVm model)
		{
			return Ok(await service.Create(session.CompanyId, model));
		}
		public async Task<string> AddUsers([GraphQLSession] AuthSession session, [Service] IProjectCommandService service,string projectId, List<string> model)
		{
			return Ok(await service.AddUsers(session.CompanyId, projectId, model));
		}
		public async Task<string> AddUser([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, string projectId, string model)
		{
			return Ok(await service.AddUser(session.CompanyId, projectId, model));
		}
		public async Task<string> Update([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, UpdateProjectVm model)
		{
			
			await service.Update(session.CompanyId, model);
			return Ok();
		}

		public async Task<string> Inactivate([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, string id)
		{
			await service.Inactivate(session.CompanyId,id);
			return Ok();
		}

		public async Task<string> Activate([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, string id)
		{
			await service.Activate(session.CompanyId, id);
			return Ok();
		}
		public async Task<string> Complete([GraphQLSession] AuthSession session, [Service] IProjectCommandService service, string id)
		{
			await service.Complete(session.CompanyId, id);
			return Ok();
		}

	}
}
