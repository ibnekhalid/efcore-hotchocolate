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

    public class CreateUserVm
    {

        [Required]
        [EmailAddress]
        public String Email { get; set; }

    }
   
}
