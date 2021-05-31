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
    public class ProjectQueryType
    {
        [Authorize]
        [UseQueryContext]
        [UseProjection]
        [UseFiltering]
        [UseFirstOrDefault]
        public IQueryable<Core.Model.Project> GetProject([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context, string projectId)
            => context.Projects.Where(x => x.CompanyId.Equals(session.CompanyId) && x.Id.Equals(projectId));
        [Authorize]
        [UseQueryContext]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Core.Model.Project> GetProjects([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context)
            => context.Projects.Where(x => x.CompanyId.Equals(session.CompanyId));


    }
    public class ProjectQueryConfiguration
        : ObjectType<Core.Model.Project>, IGraphQLType
    {
        protected override void Configure(IObjectTypeDescriptor<Core.Model.Project> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.CompanyId);
            descriptor.Field(x => x.Title);
            descriptor.Field(x => x.Status);
            descriptor.Field(x => x.Users);
            descriptor.Field(x => x.Description);
        }
    }
}
