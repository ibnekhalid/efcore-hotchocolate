using HotChocolate;

namespace Tm.Api.Extensions.GraphQL
{
    public class GraphQLSession : GlobalStateAttribute
    {
        public GraphQLSession() : base("session")
        {
        }
    }
}
