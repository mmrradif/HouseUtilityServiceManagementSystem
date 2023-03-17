using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace House_Utiliti_Service.ViewModels
{
    public class PaymentEditModel
    {
        public int PaymentId { get; set; }
        [Required, Display(Name = "Worker Id")]
        public int WorkerId { get; set; }
        [Required]
        public HttpPostedFileBase WorkerPictur { get; set; }
        [Required, Display(Name = "TotalWorkHour")]
        public float TotalWorkHour { get; set; }
        [Required, Display(Name = "TotalPayment")]
        public decimal TotalPayment { get; set; }
       
    }
}