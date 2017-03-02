using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FindAFixMVC.Models.DBContext;
using System.Collections.Generic;

namespace FindAFixMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Technician> Technicians { get; set; }
        public virtual ICollection<UserJob> UserJobs { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FindAFixMVC.Models.DBContext.UserJob> UserJobs { get; set; }

        public System.Data.Entity.DbSet<FindAFixMVC.Models.DBContext.Proposal> Proposals { get; set; }

        public System.Data.Entity.DbSet<FindAFixMVC.Models.DBContext.Technician> Technicians { get; set; }

        public System.Data.Entity.DbSet<FindAFixMVC.Models.DBContext.TechnicianProfile> TechnicianProfiles { get; set; }
    }
}