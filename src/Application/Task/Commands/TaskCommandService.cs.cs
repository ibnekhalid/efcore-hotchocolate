using Application.Company.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Identity;
using Application.Task.Commands.ViewModel;

namespace Application.Task.Commands
{
    public class TaskCommandService : ITaskCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<User> _userManager;
        public TaskCommandService(ICompanyRepository companyRepository,UserManager<User> userManager)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
        }
        public async Task<string> Create(string companyId, CreateTaskVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = company.GetProject(vm.ProjectId) ?? throw new Exception($"No Project found against id:'{vm.ProjectId}'");
             WorkItem? workItem = null;
            if (vm.ParentId is not null)
			{
                workItem = project.GetWorkItem(vm.ParentId);
			}
			
            var task = new WorkItem(project,vm.Name,vm.Description,vm.Tags,vm.Discussion,vm.Priority,vm.Status,vm.Activity,vm.EstimatedHours,vm.RemainingHours,vm.CompletedHours, workItem,"");
            project.AddTask(task);

			await _companyRepository.SaveChanges();
            return task.Id;
        }

       
       
        public async Task<string> Update(string companyId, UpdateTaskVm vm)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = company.GetProject(vm.ProjectId) ?? throw new Exception($"No Project found against id:'{vm.ProjectId}'");
            WorkItem? workItem = null;
            if (vm.ParentId is not null)
            {
                workItem = project.GetWorkItem(vm.ParentId);
            }
            var task = project.GetTask(vm.Id);
            task.Update(vm.Name, vm.Description, vm.Tags, vm.Discussion, vm.Priority, vm.Status, vm.Activity, vm.EstimatedHours, vm.RemainingHours, vm.CompletedHours, workItem, "");

            await _companyRepository.SaveChanges();
            return task.Id;
        }
        public async Task<string> Inactivate(string companyId, string projectId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = company.GetProject(projectId) ?? throw new Exception($"No Project found against id:'{projectId}'");
          
            var task = project.GetTask(id);
            task.Inactivate();
            await _companyRepository.SaveChanges();
            return task.Id;
        }
        public async Task<string> Activate(string companyId, string projectId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = company.GetProject(projectId) ?? throw new Exception($"No Project found against id:'{projectId}'");

            var task = project.GetTask(id);
            task.Activate();
            await _companyRepository.SaveChanges();
            return task.Id;
        }
        public async Task<string> Complete(string companyId, string projectId, string id)
        {
            var company = await _companyRepository.GetFirstOrDefault(companyId) ?? throw new Exception($"No Company found against id:'{companyId}'");
            var project = company.GetProject(projectId) ?? throw new Exception($"No Project found against id:'{projectId}'");

            var task = project.GetTask(id);
            task.Complete();
            await _companyRepository.SaveChanges();
            return task.Id;
        }
    }

}
