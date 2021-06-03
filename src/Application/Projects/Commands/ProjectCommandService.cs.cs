using Application.Company.Repository;
using Application.Projects.Commands.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Application.Projects.Commands
{
    public class ProjectCommandService : IProjectCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserCommandService _userCommandService;
        private readonly UserManager<User> _userManager;
        public ProjectCommandService(ICompanyRepository companyRepository, IUserCommandService userCommandService, UserManager<User> userManager)
        {
            _companyRepository = companyRepository;
            _userCommandService = userCommandService;
            _userManager = userManager;
        }
        public async Task<string> Create(string companyId, CreateProjectVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = new Core.Model.Project(company, vm.Title, vm.Description);
            company.AddProject(project);

            await _companyRepository.SaveChanges();
            return project.Id;
        }

        public async Task<string> AddUsers(string companyId, string projectId, List<string> vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var project = company.GetProject(projectId) ?? throw new Exception($"No Project found against id:'{projectId}'");

            var users = company.GetUsers(vm);
            
            project.AddUsers(users);

            await _companyRepository.SaveChanges();
            return project.Id;
        }
        public async Task<string> AddUser(string companyId, string projectId, string email)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var project = company.GetProject(projectId) ?? throw new Exception($"No Project found against id:'{projectId}'");

            var user = company.GetUserByEmail(email);
            if(user is null)
            {
               await _userCommandService.Create(companyId, new CreateUserVm { Email = email });
                user = company.GetUserByEmail(email);
            }
            
            project.AddUsers(new List<Core.Model.User> { user });

            await _companyRepository.SaveChanges();
            return project.Id;
        }
        public async Task<string> Update(string companyId, UpdateProjectVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(vm.Id) ?? throw new Exception($"No Company found against id:'{vm.Id}'");
            var project = company.GetProject(vm.Id) ?? throw new Exception($"No Project found against id:'{vm.Id}'");

            project.Update(vm.Title, vm.Description);
            await _companyRepository.SaveChanges();
            return project.Id;
        }
        public async Task<string> Inactivate(string companyId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var project = company.GetProject(id) ?? throw new Exception($"No Project found against id:'{id}'");
            project.Inactivate();
            await _companyRepository.SaveChanges();
            return project.Id;
        }
        public async Task<string> Activate(string companyId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var project = company.GetProject(id) ?? throw new Exception($"No Project found against id:'{id}'");
            project.Activate();
            await _companyRepository.SaveChanges();
            return project.Id;
        }
        public async Task<string> Complete(string companyId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");

            var project = company.GetProject(id) ?? throw new Exception($"No Project found against id:'{id}'");
            project.Complete();
            await _companyRepository.SaveChanges();
            return project.Id;
        }
    }

}
