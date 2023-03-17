using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace House_Utiliti_Service.Models
{
    public class WorkArea
    {
        [Key,Display(Name = "WorkArea Id")]
        public int WorkAreaId { get; set; }
        [Required, StringLength(60), Display(Name = "WorkArea Name")]
        public string WorkAreaName { get; set; }
        [Required, StringLength(40), Display(Name = "WorkArea Skill")]
        public string Skill { get; set; }
        public ICollection<WorkerArea> workerAreas { get; set; } = new List<WorkerArea>();
        public ICollection<Work> Works { get; set; } = new List<Work>();
    }
    public class Worker
    {
        [Key,Display(Name = "Worker Id")]
        public int WorkerId { get; set; }
        [Required, StringLength(50), Display(Name = "Worker Name")]
        public string WorkerName { get; set; }

        [Required, StringLength(20), Display(Name = "Worker Phone")]
        public String Phone { get; set; }
        [Required, Display(Name = "Payrate")]
        public decimal Payrate { get; set; }
        [Display(Name = "Available?")]
        public bool IsRunning { get; set; }
        public ICollection<WorkerArea> workerAreas { get; set; } = new List<WorkerArea>();
        public ICollection<WorkerPayment> workerPayments { get; set; } = new List<WorkerPayment>();
    }
    public class WorkerArea
    {
        [Key, Column(Order = 0), ForeignKey("Worker")]
        public int WorkerId { get; set; }
        [Key, Column(Order = 1), ForeignKey("WorkArea")]
        public int WorkAreaId { get; set; }
        public Worker Worker { get; set; }
        public WorkArea WorkArea { get; set; }
    }
    public class Work
    {
        
        public int WorkId { get; set; }
        [Required,ForeignKey("WorkArea")]
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
    public class WorkerPayment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required, ForeignKey("Worker")]
        public int WorkerId { get; set; }
        [Required, StringLength(150)]
        public string WorkerPictur { get; set; }
        [Required, Display(Name = "TotalWorkHour")]
        public float TotalWorkHour { get; set; }
        [Required, Display(Name = "TotalPayment")]
        public decimal TotalPayment { get; set; }
        public Work Work { get; set; }
        public Worker Worker { get; set; }
    }
    public class USDbContext : DbContext
    {
        public USDbContext()
        {
            Database.SetInitializer(new USDbInitializer());
        }
        public DbSet<WorkArea> workAreas { get; set; }
        public DbSet<Worker> workers { get; set; }
        public DbSet<WorkerArea> workerAreas { get; set; }
        public DbSet<Work> works { get; set; }
        public DbSet<WorkerPayment> workerPayments { get; set; }

    }
    public class USDbInitializer : DropCreateDatabaseIfModelChanges<USDbContext>
    {
        protected override void Seed(USDbContext context)
        {
            WorkArea wa = new WorkArea { WorkAreaName = "Niketan", Skill = "Plumbing" };
            Worker w = new Worker { WorkerName = "Soyeb", Phone = "01968363446", Payrate = 800.00m, IsRunning = false };

            wa.Works.Add(new Work { CustomerName = "Maruf", CustomerAddress = "Mirpur", WorkDescription = "Swage pipe replacement", StartDate= new DateTime(2022,05,01), EndDate= new DateTime(2022,01,01) });
            w.workerPayments.Add(new WorkerPayment { WorkerPictur = "1.jpg", TotalWorkHour = 22, TotalPayment = 500 });
            WorkerArea war = new WorkerArea { Worker = w, WorkArea = wa };
            context.workAreas.Add(wa);
            context.workers.Add(w);
            context.workerAreas.Add(war);
            context.SaveChanges();
        }
    }
}