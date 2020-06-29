using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.ViewModels
{
    public class TaskViewModel
    {
        [Required]
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int UserId { get; set; }
    }
}
