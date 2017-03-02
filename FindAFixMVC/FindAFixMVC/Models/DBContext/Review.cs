namespace FindAFixMVC.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        [Required]
        public string ReviewText { get; set; }

        public int TechnicianProfile_Id { get; set; }

        public int UserJob_Id { get; set; }

        public virtual TechnicianProfile TechnicianProfile { get; set; }

        public virtual UserJob UserJob { get; set; }
    }
}
