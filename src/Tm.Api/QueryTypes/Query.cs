using Common.GraphQL;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Api.QueryTypes.Company;

namespace Tm.Api.QueryTypes
{
	public class Query
	{
		
	}
	public class QueryConfiguration
		: ObjectType<Query>, IGraphQLType
	{
		protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
		{
			descriptor.Field<CompanyQueryType>(resolver =>
				resolver.GetCompany(default, default)).Name("Company");
			descriptor.Field<CompanyQueryType>(resolver =>
				resolver.GetCompanies(default)).Name("Companies");
		}
	}
}
