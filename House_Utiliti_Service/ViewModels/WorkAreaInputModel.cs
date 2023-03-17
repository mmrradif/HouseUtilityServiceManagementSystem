using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House_Utiliti_Service.ViewModels
{
    public class WorkAreaInputModel
    {
        [Display(Name = "WorkArea Id")]
        public int WorkAreaId { get; set; }
        [Required, StringLength(60), Display(Name = "WorkArea Name")]
        public string WorkAreaName { get; set; }
        [Required, StringLength(40), Display(Name = "WorkArea Skill")]
        public string Skill { get; set; }
        public List<WorkerViewModel> workers { get; set; } = new List<WorkerViewModel>();
    }
}