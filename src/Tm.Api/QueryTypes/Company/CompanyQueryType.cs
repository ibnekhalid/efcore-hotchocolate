using Common.GraphQL;
using Core;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;
using Tm.Api.Extensions;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.QueryTypes.Company
{
	public class CompanyQueryType
	{
		[Authorize]
		[UseQueryContext]
		[UseProjection]
		[UseFiltering]
		[UseFirstOrDefault]
		public IQueryable<Core.Model.Company> GetCompany([GraphQLSession] AuthSession session,[ScopedService] BaseQueryContext context,int companyId)
			=> context.Company.Where(x => x.Id.Equals(companyId));
		[Authorize]
		[UseQueryContext]
		[UsePaging]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<Core.Model.Company> GetCompanies([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context)
			=> context.Company;


	}
	public class BusinessQueryConfiguration
		: ObjectType<Core.Model.Company>, IGraphQLType
	{
		protected override void Configure(IObjectTypeDescriptor<Core.Model.Company> descriptor)
		{
			descriptor.BindFieldsExplicitly();
			descriptor.Field(x => x.Id);
			descriptor.Field(x => x.Name);
			descriptor.Field(x => x.Status);
			descriptor.Field(x => x.Users);
			descriptor.Field(x => x.Projects);
		}
	}
}
