using Common;
namespace Core.Model
{


    public class UserProject : StringEntity
    {
        public string UserId { get; protected set; }
        public string ProjectId { get; protected set; }
        public virtual Project Project { get; protected set; }
        public virtual User User { get; protected set; }
        protected UserProject()
        {

        } 
        public UserProject( User user, Project project)
        {
            UserId = user.Id;
            ProjectId = project.Id;
        }
    }
}
