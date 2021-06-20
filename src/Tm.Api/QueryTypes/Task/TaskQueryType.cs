using Common.GraphQL;
using Core;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;
using Tm.Api.Extensions;
using Tm.Api.Extensions.GraphQL;

namespace Tm.Api.QueryTypes.Task
{
    public class TaskQueryType
    {
        
        [Authorize]
        [UseQueryContext]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Core.Model.WorkItem> GetTasks([GraphQLSession] AuthSession session, [ScopedService] BaseQueryContext context,string projectId)
            => context.WorkItems.Where(x => x.ProjectId.Equals(projectId) && x.Project.CompanyId.Equals(session.CompanyId));


    }
 

    public class WorkItemQueryConfiguration
     : ObjectType<Core.Model.WorkItem>, IGraphQLType
    {
        protected override void Configure(IObjectTypeDescriptor<Core.Model.WorkItem> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Id);
           
            descriptor.Field(x => x.Status);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.Discussion);
            descriptor.Field(x => x.WorkItemType);
           
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.ParentId);
           
            descriptor.Field(x => x.Project);
         
            descriptor.Field(x => x.Tags);
            descriptor.Field(x => x.UpdateHistory);
            descriptor.Field(x => x.Parent);
            descriptor.Field(x => x.Children);
            descriptor.Field(x => x.Parent);

        }
    }
}
