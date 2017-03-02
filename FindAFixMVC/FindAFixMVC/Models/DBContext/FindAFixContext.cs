namespace FindAFixMVC.Models.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FindAFixContext : DbContext
    {
        public FindAFixContext()
            : base("name=FindAFixContext")
        {
        }

        public virtual DbSet<Proposal> Proposals { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<TechnicianProfile> TechnicianProfiles { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }
        public virtual DbSet<UserJob> UserJobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proposal>()
                .Property(e => e.PriceEst)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TechnicianProfile>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.TechnicianProfile)
                .HasForeignKey(e => e.TechnicianProfile_Id);

            modelBuilder.Entity<Technician>()
                .HasMany(e => e.Proposals)
                .WithRequired(e => e.Technician)
                .HasForeignKey(e => e.Technician_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Technician>()
                .HasMany(e => e.UserJobs)
                .WithOptional(e => e.Technician)
                .HasForeignKey(e => e.Technician_Id);

            modelBuilder.Entity<UserJob>()
                .HasMany(e => e.Proposals)
                .WithRequired(e => e.UserJob)
                .HasForeignKey(e => e.UserJob_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserJob>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.UserJob)
                .HasForeignKey(e => e.UserJob_Id);


            modelBuilder.Entity<Technician>()
                .HasRequired(n => n.AspNetUser)
                .WithMany(a => a.Technicians)
                .HasForeignKey(n => n.AspNetUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserJob>()
                .HasRequired(n => n.AspNetUser)
                .WithMany(a => a.UserJobs)
                .HasForeignKey(n => n.AspNetUser)
                .WillCascadeOnDelete(false);
        }
    }
}
