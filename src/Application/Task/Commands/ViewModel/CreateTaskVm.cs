using Common;
using System.ComponentModel.DataAnnotations;

namespace Application.Task.Commands.ViewModel
{

	public class CreateTaskVm 
    {
       
        [Required]
        public virtual string Name { get;  set; }
        public virtual string Description { get;  set; }
        public virtual string Discussion { get;  set; }
        public virtual string Tags { get;  set; }
        public virtual Status Status { get;  set; }
        public virtual string ParentId { get;  set; }
        [Required]
        public virtual string ProjectId { get;  set; }
        public byte Priority { get; set; }
        public Activity Activity { get; set; }
        public byte EstimatedHours { get; set; }
        public byte RemainingHours { get; set; }
        public byte CompletedHours { get; set; }
    }
    public class UpdateTaskVm: CreateTaskVm
    {
        [Required]
        public string Id { get; set; }
       
    }
}
