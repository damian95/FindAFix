using FindAFixMVC.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindAFixMVC.Models.ViewModels;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.BuilderProperties;
using Microsoft.AspNet.Identity;



namespace FindAFixMVC.Models.Repositories
{
    public class JobsRepository : JobIRepository<UserJob>
    {
        //private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public JobsRepository()
        {

        }

        public void DeleteJob(UserJob job)
        {
            db.UserJobs.Remove(job);
            db.SaveChanges();
        }

        public UserJob GetJob(int? id)
        {
            return db.UserJobs.Find(id);
        }

        public List<UserJob> GetJobs()
        {
            return db.UserJobs.ToList();
        }

        public List<UserJob> getJobsByTechId(int id)
        {
            List<UserJob> _list = new List<UserJob>();
            var jobs = from UserJob in db.UserJobs
                       where UserJob.Technician_Id == id
                       select UserJob;
            foreach (var job in jobs)
            {
                _list.Add(job);
            }
            return _list;
        }

        public List<UserJob> getJobsByUserId(string id)
        {
            List<UserJob> _list = new List<UserJob>();
            var uJobs = from UserJob in db.UserJobs
                        where UserJob.AspNetUser_Id == id
                        select UserJob;
            foreach (var job in uJobs)
            {
                _list.Add(job);
            }
            return _list;
        }

        public Proposal GetProposal(int id)
        {
            return db.Proposals.Find(id);
        }

        public List<Proposal> getProposalByJobId(int id)
        {
            List<Proposal> _list = new List<Proposal>();
            var jobProps = from Proposal in db.Proposals
                           where Proposal.UserJob_Id == id
                           select Proposal;
            foreach (var prop in jobProps)
            {
                _list.Add(prop);
            }
            return _list;
        }

        public Technician GetTech(int id)
        {
            return db.Technicians.Find(id);
        }

        public int getTechIdByUserId(string id)
        {
            var techid = (from Technician in db.Technicians
                          where Technician.AspNetUser_Id == id
                          select Technician.Id).FirstOrDefault();
            return techid;
        }

        public List<UserJob> getTechJobsByTechId(int id)
        {
            List<UserJob> _list = new List<UserJob>();
            var tJobs = from UserJob in db.UserJobs
                        where UserJob.Technician_Id == id
                        select UserJob;
            foreach (var job in tJobs)
            {
                _list.Add(job);
            }
            return _list;
        }

        public List<CategoryJobsViewModel> getTechsByCategory(string category)
        {
            List<CategoryJobsViewModel> _techs = new List<CategoryJobsViewModel>();
            var techs = from Technician in db.Technicians
                        where Technician.JobType == category
                        select Technician;

            foreach (var item in techs)
            {
                var profile = (from TechnicianProfile in db.TechnicianProfiles
                               where TechnicianProfile.techId == item.Id
                               select TechnicianProfile).FirstOrDefault();

                CategoryJobsViewModel _model = new CategoryJobsViewModel()
                {
                    Id = profile.Id,
                    techid = item.Id,
                    techAspnetId = item.ApplicationUser_Id,
                    CompanyName = item.CompanyName,
                    Rating = profile.Rating
                };
                _techs.Add(_model);
            }

            return _techs;
        }

        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public void PostJob(UserJob job)
        {
            db.UserJobs.Add(job);
            db.SaveChanges();
        }

        public void PostProposal(Proposal proposal)
        {
            db.Proposals.Add(proposal);
            db.SaveChanges();
        }

        public void PutJob(UserJob job)
        {
            db.Entry(job).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void PutProposal(Proposal proposal)
        {
            db.Entry(proposal).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}