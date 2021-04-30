using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Api.MutationType.Company;

namespace Tm.Api.MutationType
{
    public class BaseMutationType
    {
        protected string Ok(string value = null)
            => value?.Length<1 ? "200" : value;
        protected Task<string> OkResult(string value = null)
            => Task.FromResult(value?.Length < 1 ? "200" : value);
        protected int Ok(int value)
            => value;
        protected Task<string> OkResult(Guid value)
            => Task.FromResult(value.ToString());
    }
    public class Mutation
    {
        [Authorize]
        public CompanyMutationType Company => new CompanyMutationType();
        public AccountMutationType Account => new AccountMutationType();
       

    }
}
