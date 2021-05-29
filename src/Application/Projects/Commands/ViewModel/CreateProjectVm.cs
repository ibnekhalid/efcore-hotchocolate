using Application.Company.Commands.ViewModel;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands.ViewModel
{
   
    public class CreateProjectVm 
    {
       
        [Required]
        public virtual string Title { get;  set; }
        [Required]
        public virtual string Description { get;  set; }
    }
    public class UpdateProjectVm: CreateProjectVm
    {
        public string Id { get; set; }
    }
}
