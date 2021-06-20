using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Api.MutationType.Company;
using Tm.Api.MutationType.Project;
using Tm.Api.MutationType.Task;

namespace Tm.Api.MutationType
{
    public class BaseMutationType
    {
        protected string Ok(string value = null)
            => value?.Length<1 ? "200" : value;
        protected Task<string> OkResult(string value = null)
            => System.Threading.Tasks.Task.FromResult(value?.Length < 1 ? "200" : value);
        protected int Ok(int value)
            => value;
        protected Task<string> OkResult(Guid value)
            => System.Threading.Tasks.Task.FromResult(value.ToString());
    }
    public class Mutation
    {
        [Authorize]
        public CompanyMutationType Company => new CompanyMutationType();
        public AccountMutationType Account => new AccountMutationType();
        [Authorize]
        public ProjectMutationType Project => new ProjectMutationType();
        [Authorize]
        public UserMutationType User => new UserMutationType();
        [Authorize]
        public TaskMutationType Task => new TaskMutationType();


    }
}
