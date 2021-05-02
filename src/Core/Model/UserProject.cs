using Common;
namespace Core.Model
{


    public class UserProject : StringEntity
    {
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public Status Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
