using Common;
using System.Collections.Generic;
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
        public virtual IList<User> Users { get; protected set; } = new List<User>();
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
		{
            var usersList = Users.ToList();
            users.ForEach(u =>
            {
                
                usersList.Add(u);
                Users = users;
            });
           
        }
        public void RemoveUsers(List<User> users)
        {
            var userIds = users.Select(x => x.Id).ToList();
            userIds.ForEach(id =>
            {
                var user = Users.AsEnumerable().FirstOrDefault(x => x.Id == id);
                Users.Remove(user);
            });
            
        }
        public void Inactivate()
           => Status = Status.Inactive;
        public void Activate()
            => Status = Status.Active;
        public void Complete()
         => Status = Status.Complete;
        #endregion
    }

}
