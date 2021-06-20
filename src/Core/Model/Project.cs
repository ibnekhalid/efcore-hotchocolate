using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core.Model
{
	public class Project : StringEntity
	{
		#region Properties
		public string CompanyId { get; protected set; }
		public string Title { get; protected set; }
		public string Description { get; protected set; }
		public Status Status { get; protected set; }
		#endregion
		#region Navigations
		public virtual Company Company { get; protected set; }
		public virtual List<UserProject> UserProjects { get; set; } = new List<UserProject>();
	
		public virtual List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
		#endregion
		#region Constructors
		protected Project()
		{

		}
		public Project(Company company, string title, string description)
		{
			CompanyId = company.Id;
			Title = title;
			Description = description;
			Activate();

		}
		#endregion

		#region Behaviour
		public void Update(string title, string description)
		{

			Title = title;
			Description = description;

		}
		public void AddUsers(List<User> users)
			=> UserProjects.AddRange(users.Select(x => new UserProject(x, this)));
		public void RemoveUsers(List<User> users)
		{
			var userIds = users.Select(x => x.Id).ToList();
			UserProjects.RemoveAll(x => userIds.Contains(x.UserId));
		}
		public void Inactivate()
		   => Status = Status.Inactive;
		public void Activate()
			=> Status = Status.Active;
		public void Complete()
		 => Status = Status.Complete;


        #region Tasks
        public WorkItem GetWorkItem(string id)
            => WorkItems.FirstOrDefault(x => x.Id.Equals(id));
        public WorkItem GetTask(string id)
			=> WorkItems.FirstOrDefault(x => x.Id.Equals(id));
		public void AddTask(WorkItem task)
			=> WorkItems.Add(task);

		#endregion
		#endregion
	}

}
