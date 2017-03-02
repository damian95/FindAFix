using FindAFixMVC.Models.DBContext;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.Repositories
{
    public class HomeRepository : HomeIRepository<Technician>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string getUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public Technician GetTechByUserId(string id)
        {
            var tech = (from Technician in db.Technicians
                        where Technician.AspNetUser_Id == id
                        select Technician).FirstOrDefault();
            return tech;
        }

        public int GetTechIdByUserId(string id)
        {
            var techid = (from Technician in db.Technicians
                          where Technician.ApplicationUser_Id == id
                          select Technician.Id).FirstOrDefault();
            return techid;
        }

        public TechnicianProfile GetTechProfileByTechId(int id)
        {
            var profile = (from TechnicianProfile in db.TechnicianProfiles
                           where TechnicianProfile.techId == id
                           select TechnicianProfile).FirstOrDefault();
            return profile;
        }

        public void PostTech(Technician tech)
        {
            db.Technicians.Add(tech);
            db.SaveChanges();
        }

        public void PostTechProfile(TechnicianProfile profile)
        {
            db.TechnicianProfiles.Add(profile);
            db.SaveChanges();
        }
    }
}