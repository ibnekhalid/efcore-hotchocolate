using Application.Task.Commands;
using Application.Task.Commands.ViewModel;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Api.Extensions;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.MutationType.Task
{
	public class TaskMutationType : BaseMutationType
	{
		public async Task<string> Add([GraphQLSession] AuthSession session, [Service] ITaskCommandService service, CreateTaskVm model)
		{
			return Ok(await service.Create(session.CompanyId, model));
		}
		
		
		public async Task<string> Update([GraphQLSession] AuthSession session, [Service] ITaskCommandService service, UpdateTaskVm model)
		{
			
			await service.Update(session.CompanyId, model);
			return Ok();
		}

		public async Task<string> Inactivate([GraphQLSession] AuthSession session, [Service] ITaskCommandService service,string projectId, string id)
		{
			await service.Inactivate(session.CompanyId, projectId,id);
			return Ok();
		}

		public async Task<string> Activate([GraphQLSession] AuthSession session, [Service] ITaskCommandService service, string projectId, string id)
		{
			await service.Activate(session.CompanyId, projectId, id);
			return Ok();
		}
		public async Task<string> Complete([GraphQLSession] AuthSession session, [Service] ITaskCommandService service, string projectId, string id)
		{
			await service.Complete(session.CompanyId, projectId, id);
			return Ok();
		}

	}
}
