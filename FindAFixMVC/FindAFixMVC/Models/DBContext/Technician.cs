namespace FindAFixMVC.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Technician
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Technician()
        {
            Proposals = new HashSet<Proposal>();
            UserJobs = new HashSet<UserJob>();
        }

        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CEOFirstName { get; set; }

        [Required]
        public string CEOLastName { get; set; }

        [Required]
        public string JobType { get; set; }

        [Required]
        [StringLength(128)]
        public string AspNetUser_Id { get; set; }

        public int ProposalId { get; set; }

        [StringLength(128)]
        public string ApplicationUser_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proposal> Proposals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserJob> UserJobs { get; set; }

        public virtual ApplicationUser AspNetUser { get; set; }

        
    }
}
