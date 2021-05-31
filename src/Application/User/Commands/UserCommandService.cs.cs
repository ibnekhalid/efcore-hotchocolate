using Application.Company.Repository;
using Application.Projects.Commands.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public class UserCommandService : IUserCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        readonly UserManager<Core.Model.User> _userManager;
        public UserCommandService(ICompanyRepository companyRepository, UserManager<Core.Model.User> userManager)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
        }
        public async Task<string> Create(string companyId, CreateUserVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var user = new Core.Model.User(company, vm.Email, vm.Email);

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
                throw new Exception(identityResult?.Errors?.First()?.Description);
            return user.Id;
        }

        public async Task<string> Inactivate(string companyId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var user = company.GetUser(id) ?? throw new Exception($"No User found against id:'{id}'");
            user.Inactivate();
            await _companyRepository.SaveChanges();
            return user.Id;
        }
        public async Task<string> Activate(string companyId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var user = company.GetUser(id) ?? throw new Exception($"No User found against id:'{id}'");
            user.Activate();
            await _companyRepository.SaveChanges();
            return user.Id;
        }
    }

}
