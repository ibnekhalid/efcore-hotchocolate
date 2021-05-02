using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution.Options;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Execution.Batching;
using Common.GraphQL;
using Tm.Api.QueryTypes;
using Tm.Api.MutationType;

namespace Tm.Api.Extensions.GraphQL
{
    public static class GraphQLServiceExtension
	{
		public static string Path { get; private set; }
		public static IRequestExecutorBuilder RegisterTypes(this IRequestExecutorBuilder graphqlServer, Type[] types, string path = "/query")
		{
			Path = path;
			graphqlServer
				.AddQueryType<Query>()
				.AddMutationType<Mutation>()
				.AddType<ExportDirectiveType>();
			types
				.Where(type => typeof(IGraphQLType).IsAssignableFrom(type) && !type.IsInterface).ToList()
				.ForEach(type => graphqlServer.AddType(type));

			return graphqlServer;
		}
		// ReSharper disable once InconsistentNaming
		public static IServiceCollection RegisterGraphQL(this IServiceCollection services, Type[] types, bool exceptionDetails = true)
		{
			services.AddGraphQLServer()
				.BindRuntimeType<Guid, GuidTypeConverter>()
				.RegisterTypes(types)
				 
				.SetConfigurations(exceptionDetails);
			services.AddErrorFilter<GraphQlErrorFilter>();
			return services;
		}
		public static IRequestExecutorBuilder SetConfigurations(this IRequestExecutorBuilder graphqlServer, bool includeExceptionDetail)
		{
			graphqlServer
				.AddFiltering()
				.AddSorting()
				.AddProjections()
				.SetPagingOptions(new PagingOptions
				{
					IncludeTotalCount = true,
					DefaultPageSize = 12,
					MaxPageSize = 100
				})
				.AddAuthorization()
				.AddHttpRequestInterceptor((ctx, executor, builder, token) =>
				{
					var identity = ctx?.GetUser()?.Identity;
					if (identity is not null && identity.IsAuthenticated)
					{
						builder.SetProperty("session", new AuthSession(ctx.User));
					}
					return ValueTask.CompletedTask;


				})
				.SetRequestOptions(provider => new RequestExecutorOptions { IncludeExceptionDetails = includeExceptionDetail });
				// Due to error in schema stiching, Uuid is already registerend on another type
				//.ConfigureSchema(x => x.BindClrType(typeof(Guid), typeof(UuidType)))
				//.AddDirectiveMergeHandler<DirectiveMerger>();
			return graphqlServer;
		}
		public static void UseCustomVoyager(this IApplicationBuilder app, string voyagerPath = "/voyager")
		{
			app.UseVoyager(new VoyagerOptions
			{
				Path = voyagerPath,
				QueryPath = Path
			});
		}
		// ReSharper disable once InconsistentNaming
		public static void RegisterGraphQLRoute(this IEndpointRouteBuilder endpoints, bool enableTool = true)
		{
			endpoints
				.MapGraphQL(Path)
				.WithOptions(new GraphQLServerOptions
				{
					Tool = { Enable = enableTool }
				});
		}
	}
}
