using Application.Account;
using Application.Account.ViewModels;
using Application.Company.Commands;
using Application.Company.Commands.ViewModel;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.MutationType.Company
{
	public class AccountMutationType : BaseMutationType
	{
		public async Task<string> Login([Service] IAccountCommandService service, LoginModel model)
		{
			return Ok(await service.GetToken(model));
		}

		public async Task<string> Register([Service] IAccountCommandService service, RegisterModel model)
		{

			await service.Register(model);
			return Ok();
		}
		[Authorize]
		public async Task<string> RefreshToken([GraphQLSession] ClaimsPrincipal context, [Service] IAccountCommandService service)
		{

			await service.RefreshToken(context);
			return Ok();
		}
		public string Logout([Service] IAccountCommandService service)
		{
			service.Logout();
			return Ok();
		}
	}
}
