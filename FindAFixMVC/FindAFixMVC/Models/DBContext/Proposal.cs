namespace FindAFixMVC.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Proposal
    {

        public int Id { get; set; }

        public decimal PriceEst { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime AvailabilityDate { get; set; }

        public int UserJob_Id { get; set; }

        public int Technician_Id { get; set; }
        
        public bool UserSeen { get; set; }

        public bool TechSeen { get; set; }

        public virtual Technician Technician { get; set; }

        public virtual UserJob UserJob { get; set; }
    }
}
