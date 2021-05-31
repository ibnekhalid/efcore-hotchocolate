using Common.GraphQL;
using HotChocolate.Types;
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
                resolver.GetCompanies(default, default)).Name("Companies");
            descriptor.Field<ProjectQueryType>(resolver =>
                resolver.GetProject(default, default, default)).Name("Project");
            descriptor.Field<ProjectQueryType>(resolver =>
            resolver.GetProjects(default, default)).Name("Projects");

            descriptor.Field<UserQueryType>(resolver =>
            resolver.GetUsers(default, default)).Name("Users");

            descriptor.Field<UserQueryType>(resolver =>
            resolver.GetUser(default, default, default)).Name("User");
        }
    }
}
