using Application.Projects.Commands;
using Application.Projects.Commands.ViewModel;
using HotChocolate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Api.Extensions;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.MutationType.Project
{
	public class UserMutationType : BaseMutationType
	{
		public async Task<string> Add([GraphQLSession] AuthSession session, [Service] IUserCommandService service, CreateUserVm model)
		{
			return Ok(await service.Create(session.CompanyId, model));
		}
		
		public async Task<string> Inactivate([GraphQLSession] AuthSession session, [Service] IUserCommandService service, string id)
		{
			await service.Inactivate(session.CompanyId,id);
			return Ok();
		}

		public async Task<string> Activate([GraphQLSession] AuthSession session, [Service] IUserCommandService service, string id)
		{
			await service.Activate(session.CompanyId, id);
			return Ok();
		}
	

	}
}
