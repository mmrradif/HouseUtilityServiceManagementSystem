using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace House_Utiliti_Service.ViewModels
{
    public class WorkerViewModel
    {
        [Required]
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }

        public String Phone { get; set; }

        public decimal Payrate { get; set; }

        public bool IsRunning { get; set; }
    }
}