using Application.Company.Commands;
using Application.Company.Commands.ViewModel;
using HotChocolate;
using System;
using System.Threading.Tasks;

namespace Tm.Api.MutationType.Company
{
	public class CompanyMutationType : BaseMutationType
	{
		public async Task<int> Add( [Service] ICompanyCommandService service, CreateCompanyVm model)
		{
			return Ok(await service.Create(model));
		}

		public async Task<string> Update([Service] ICompanyCommandService service, UpdateCompanyVm model)
		{
			
			await service.Update(model);
			return Ok();
		}

		public async Task<string> Inactivate([Service] ICompanyCommandService service, int id)
		{
			await service.Inactivate(id);
			return Ok();
		}

		public async Task<string> Activate([Service] ICompanyCommandService service, int id)
		{
			await service.Activate(id);
			return Ok();
		}

	}
}
