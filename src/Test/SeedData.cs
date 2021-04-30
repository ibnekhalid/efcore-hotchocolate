using Core.Model;
using Persistent;

namespace Test
{
    public static class SeedData
    {
        public static string CompanyId;
        public static string ProjectId;
        public static string UserId;


        public static void SeedSampleData(this BaseCommandContext context)
        {
            var company = new Core.Model.Company("Test");
            CompanyId = company.Id;

            var user = new User(company,"foo","bar");
            UserId = user.Id;

            context.Company.Add(company);
            context.Users.Add(user);

            context.SaveChangesAsync().Wait();
        }

    }
}
