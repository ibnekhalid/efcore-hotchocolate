using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public partial class WorkItem
    {
        public byte Priority { get; protected set; }
        public Activity Activity { get; protected set; }
        public byte EstimatedHours { get; protected set; }
        public byte RemainingHours { get; protected set; }
        public byte CompletedHours { get; protected set; }

        protected WorkItem()
        {

        }

        public WorkItem(Project project, string name)
        {
            ProjectId = project.Id;
            Name = name;
            New();
        }
        public WorkItem(Project project, string name, string description, string tags, string discussion, byte priority, Status status, Activity activity, byte estimatedHours, byte remainingHours, byte completedHours, WorkItem? parent, string history)
        {
            WorkItemType = WorkItemType.Task;
            ProjectId = project.Id;
            Name = name;
            Description = description;
            Tags = tags;
            Discussion = discussion;
            Priority = priority;
            Activity = activity;
            EstimatedHours = estimatedHours;
            RemainingHours = remainingHours;
            CompletedHours = completedHours;
            Status = status;
            UpdateHistory = history;
            if (parent is not null)
                ParentId = parent.Id;
        }
        public void Update(string name, string description, string tags, string discussion, byte priority, Status status, Activity activity, byte estimatedHours, byte remainingHours, byte completedHours, WorkItem? parent, string history)
        {
            Name = name;
            Description = description;
            Tags = tags;
            Discussion = discussion;
            Priority = priority;
            Activity = activity;
            EstimatedHours = estimatedHours;
            RemainingHours = remainingHours;
            CompletedHours = completedHours;
            Status = status;
            UpdateHistory = history;
            if (parent is not null)
                ParentId = parent.Id;
        }
        public void New()
           => Status = Status.New;
        public void Inactivate()
           => Status = Status.Inactive;
        public void Activate()
            => Status = Status.Active;
        public void Complete()
         => Status = Status.Complete;
    }
}
