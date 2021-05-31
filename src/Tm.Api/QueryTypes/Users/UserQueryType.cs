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
    public class UserQueryType
    {
        [Authorize]
        [UseQueryContext]
        [UseProjection]
        [UseFiltering]
        [UseFirstOrDefault]
        public IQueryable<Core.Model.User> GetUser([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context, string UserId)
            => context.Users.Where(x => x.CompanyId.Equals(session.CompanyId) && x.Id.Equals(UserId));

        [Authorize]
        [UseQueryContext]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Core.Model.User> GetUsers([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context)
            => context.Users.Where(x => x.CompanyId.Equals(session.CompanyId));


    }
    public class UserQueryConfiguration
        : ObjectType<Core.Model.User>, IGraphQLType
    {
        protected override void Configure(IObjectTypeDescriptor<Core.Model.User> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.CompanyId);
            descriptor.Field(x => x.Email);
            descriptor.Field(x => x.Status);
            descriptor.Field(x => x.EmailConfirmed);
            descriptor.Field(x => x.PhoneNumber);
            descriptor.Field(x => x.PhoneNumberConfirmed);
            descriptor.Field(x => x.UserName);
            descriptor.Field(x => x.UserProjects);
            descriptor.Field(x => x.Company);
        }
    }
    public class UserProjectQueryConfiguration
        : ObjectType<Core.Model.UserProject>, IGraphQLType
    {
        protected override void Configure(IObjectTypeDescriptor<Core.Model.UserProject> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.Project);
            descriptor.Field(x => x.ProjectId);
            descriptor.Field(x => x.User);
            descriptor.Field(x => x.UserId);
            
        }
    }
}
