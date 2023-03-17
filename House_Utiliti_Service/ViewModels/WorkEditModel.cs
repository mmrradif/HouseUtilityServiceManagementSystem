using House_Utiliti_Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace House_Utiliti_Service.ViewModel
{
    public class WorkEditModel
    {
        public int WorkId { get; set; }
        [Required, Display(Name = "WorkArea Id")]
        public int WorkAreaId { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }
        [Required, StringLength(50), Display(Name = "Work Description")]
        public string WorkDescription { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public WorkArea WorkArea { get; set; }
    }
}