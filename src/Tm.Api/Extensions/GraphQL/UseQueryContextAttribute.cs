using Core;
using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Tm.Api.Extensions.GraphQL
{
    public class UseQueryContextAttribute : ObjectFieldDescriptorAttribute
	{
		public override void OnConfigure(
			IDescriptorContext context,
			IObjectFieldDescriptor descriptor,
			MemberInfo member)
		{
			descriptor.UseDbContext<BaseQueryContext>();
		}
	}
}
