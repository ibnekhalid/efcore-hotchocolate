using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public class Company : StringEntity
    {
        #region Properties
        public string Name { get; protected set; }
        public Status Status { get; protected set; }
        #endregion

        #region Constructors
        protected Company() { }
        public Company(string name)
        {
            Name = name;
            Activate();
        }
        #endregion

        #region Navigations
        public virtual List<User> Users { get; protected set; } = new List<User>();
        public virtual List<Project> Projects { get; protected set; } = new List<Project>();
        #endregion

        #region Behavior
        public void Update(string name)
        {
            if (Status == Status.Inactive)
                throw new Exception("Company is inactive.");
            Name = name;
        }
        public void Inactivate()
            => Status = Status.Inactive;
        public void Activate()
            => Status = Status.Active;

        #region User
        public User GetUser(int id)
            => Users.FirstOrDefault(x => x.Id.Equals(id));
        public User GetUser(string email)
          => Users.FirstOrDefault(x => x.Email.Equals(email));
        public void AddUser(User user)
        {
            if (Users.Any(x => x.Email.ToLower().Equals(user.Email.ToLower()))) throw new Exception($"user with email '{user.Email}' already exists.");
            Users.Add(user);
        }
        public void RemoveUser(int userId)
        {
            var user = Users.FirstOrDefault(x => x.Id.Equals(userId)) ?? throw new Exception($"user with Id '{userId}' not found.");
            Users.Add(user);
        }

        #endregion

        #region Project
        public Project GetProject(int id)
           => Projects.FirstOrDefault(x => x.Id.Equals(id));
        public void AddProject(Project project)
        {
            if (Projects.Any(x => x.Title.ToLower().Equals(project.Title.ToLower()))) throw new Exception($"project with email '{project.Title}' already exists.");
            Projects.Add(project);
        }
        public void RemoveProject(int projectId)
        {
            var project = Projects.FirstOrDefault(x => x.Id.Equals(projectId)) ?? throw new Exception($"project with Id '{projectId}' not found.");
            Projects.Add(project);
        }

        #endregion


        #endregion
    }
}
