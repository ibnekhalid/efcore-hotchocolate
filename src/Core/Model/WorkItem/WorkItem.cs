using Common;
using System.Collections.Generic;

namespace Core.Model
{
    public partial class WorkItem : StringEntity
    {
        public string Name { get; protected set; }
        public string Tags { get; protected set; }
        public string Description { get; protected set; }
        public string Discussion { get; protected set; }
        public Status Status { get; protected set; }
        public WorkItemType WorkItemType { get; set; }
        public string? ParentId { get; protected set; }
        public string ProjectId { get; protected set; }
        public string UpdateHistory { get; protected set; }
        public virtual Project Project { get; protected set; }
        public virtual WorkItem Parent { get; protected set; }
        public virtual List<WorkItem> Children { get; protected set; } = new List<WorkItem>();
    }
   

}
