using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindAFixMVC.Models;
using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.ViewModels;
using FindAFixMVC.Models.Repositories;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;

namespace FindAFixMVC.Controllers
{
    public class JobsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private JobIRepository<UserJob> repo;

        public JobsController(JobIRepository<UserJob> _repo)
        {
            this.repo = _repo;
        }

        public JobsController() : this(new JobsRepository())
        {

        }

        public JobsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Jobs
        [Authorize]
        public ActionResult Index()
        {
            List<UserJob> jobList = new List<UserJob>();
            //store the loged in user's id
            var uid = repo.GetUserId();

            //if the loged  in is a regular user return all the jobs the have created
            if (User.IsInRole("User"))
            {
                jobList = repo.getJobsByUserId(uid);
            }
            //if the logged in user is a technician return all the jobs assigned to them
            else if (User.IsInRole("Technician"))
            {
                var techid = repo.getTechIdByUserId(uid);
                jobList = repo.getTechJobsByTechId(techid);
            }
            return View(jobList);

        }

        // GET: Jobs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserJob userJob = repo.GetJob(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            return View(userJob);
        }

        [HttpPost]
        [Authorize(Roles = "Technician")]
        public ActionResult Details(Proposal proposal)
        {

            if (ModelState.IsValid)
            {
                proposal.UserSeen = false;
                proposal.TechSeen = false;

                var uid = repo.GetUserId();

                //get the jobs that is being proposed for
                var proposalJob = repo.GetJob(proposal.UserJob_Id);

                var techId = repo.getTechIdByUserId(uid);

                //set proposal job and technician
                proposal.UserJob = proposalJob;
                proposal.Technician_Id = techId;
                // add proposal ot database
                repo.PostProposal(proposal);
            }

            return RedirectToAction("ClosestJobs");
        }

        // GET: Jobs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.error = "none";
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="User")]
        public ActionResult Create([Bind(Include = "JobTitle,JobDecription,JobType,Date,Latitude,Longitude")] JobViewModel _jobViewModel )
        {
            if (ModelState.IsValid)
            {
                //populate userJob object from database with viewmodel, add to database and redirect to index
                UserJob userJob = new UserJob
                {
                    JobTitle = _jobViewModel.JobTitle,
                    JobDescription = _jobViewModel.JobDecription,
                    JobType = _jobViewModel.JobType,
                    Date = DateTime.Now,
                    Latitude = _jobViewModel.Latitude,
                    Longitude = _jobViewModel.Longitude,
                    AspNetUser_Id = repo.GetUserId()
            };
                repo.PostJob(userJob);
                return RedirectToAction("Index");
            }

            //if text field is empty or location isn't set, set viewbag message to display in the view
            if (isNullOrEmpty(_jobViewModel.JobTitle) || isNullOrEmpty(_jobViewModel.JobDecription) ||
                isNullOrEmpty(_jobViewModel.JobType))
            {
                ViewBag.error = "empty";
                return View(_jobViewModel);
            }
            else if (_jobViewModel.Latitude == 0.0 || _jobViewModel.Longitude == 0.0)
            {
                ViewBag.error = "location";
                return View(_jobViewModel);
            }

            return View(_jobViewModel);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserJob userJob = repo.GetJob(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            return View(userJob);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit([Bind(Include = "Id,JobTitle,JobDecription,JobType,Date,Latitude,Longitude,AspNetUser_Id")] UserJob userJob)
        {
            if (ModelState.IsValid)
            {
                repo.PutJob(userJob);
                return RedirectToAction("Index");
            }
            return View(userJob);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserJob userJob = repo.GetJob(id);
            if (userJob == null)
            {
                return HttpNotFound();
            }
            return View(userJob);
        }

        // POST: Jobs/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserJob userJob = repo.GetJob(id);
            repo.DeleteJob(userJob);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AvailableJobs()
        {
            //decalre list to store releveant jobs
            List<UserJob> jobList = new List<UserJob>();
            //get jobs iwth technician assigned
            jobList = repo.getJobsByTechId(0);
            return View(jobList);
        }

        [HttpPost]
        [Authorize(Roles = "Technician")]
        public ActionResult AvailableJobs(Proposal proposal)
        {
            List<UserJob> jobList = new List<UserJob>();

            if (ModelState.IsValid)
            {
                proposal.UserSeen = false;
                proposal.TechSeen = false;

                var uid = repo.GetUserId();

                //get the jobs that is being proposed for
                var job = repo.GetJob(proposal.UserJob_Id);

                //get the jobs with no technician assigned to it
                jobList = repo.getJobsByTechId(0);

                var techId = repo.getTechIdByUserId(uid);

                //set proposal job and technician
                proposal.UserJob = job;
                proposal.Technician_Id = techId;
                //add the job to the proposal
                job.Proposals.Add(proposal);
                // add proposal ot database
                repo.PostProposal(proposal);
            }                

            return View(jobList);
        }

        public ActionResult Proposals(int? id)
        {
            List<Proposal> _proposalList = new List<Proposal>();

            int jobid = Convert.ToInt32(id);
            var jobProps = repo.getProposalByJobId(jobid);

            foreach (var proposal in jobProps)
            {
                if (!proposal.UserSeen)
                {
                    proposal.UserSeen = true;
                }
                _proposalList.Add(proposal);

                repo.PutProposal(proposal);
            }

            return View(_proposalList);
        }

        public ActionResult ClosestJobs()
        {
            List<UserJob> coords = new List<UserJob>();
            coords = repo.GetJobs();
            return View(coords);
        }

        public ActionResult AcceptProposal(int id)
        {
            var pid = Convert.ToInt32(id);
            var uid = repo.GetUserId();
            //select the proposal being accepted
            var proposal = repo.GetProposal(pid);

            //select the technician associated witht he proposal
            var tech = repo.GetTech(proposal.Technician_Id);

            //select the job associated with the proposal being accepted
            var job = repo.GetJob(proposal.UserJob_Id);

            job.Technician_Id = tech.Id;
            job.Technician = tech;
            repo.PutJob(job);
            return RedirectToAction("Index");
        }

        public ActionResult CategoryJobs(int id)
        {
            if (id == 1)
            {
                ViewBag.Category = "Basement Finishing";
                return View(repo.getTechsByCategory("Basement Finishing"));
            }
            else if (id == 2)
            {
                ViewBag.Category = "Fencing";
                return View(repo.getTechsByCategory("Fencing"));
            }
            else if (id == 3)
            {
                ViewBag.Category = "Floors and Walls";
                return View(repo.getTechsByCategory("Floors and Walls"));
            }
            else if (id == 4)
            {
                ViewBag.Category = "Gardening";
                return View(repo.getTechsByCategory("Gardening"));
            }
            else if (id == 5)
            {
                ViewBag.Category = "Plumbing";
                return View(repo.getTechsByCategory("Plumbing"));
            }
            else if (id == 6)
            {
                ViewBag.Category = "Windows";
                return View(repo.getTechsByCategory("Windows"));
            }
            return View();
        }

        //public List<CategoryJobsViewModel> getTechsByCategory(String category)
        //{
        //    List<CategoryJobsViewModel> _techs = new List<CategoryJobsViewModel>();
        //    var techs = from Technician in db.Technicians
        //                where Technician.JobType == category
        //                select Technician;

        //    foreach (var item in techs)
        //    {
        //        var profile = (from TechnicianProfile in db.TechnicianProfiles
        //                       where TechnicianProfile.techId == item.Id
        //                       select TechnicianProfile).FirstOrDefault();

        //        CategoryJobsViewModel _model = new CategoryJobsViewModel()
        //        {
        //            Id = profile.Id,
        //            techid = item.Id,
        //            techAspnetId = item.ApplicationUser_Id,
        //            CompanyName = item.CompanyName,
        //            Rating = profile.Rating
        //        };
        //        _techs.Add(_model);
        //    }

        //    return _techs;
        //}

        //public List<UserJob> getJobsByUserId(string id)
        //{
        //    List<UserJob> _list = new List<UserJob>();
        //    var uJobs = from UserJob in db.UserJobs
        //                where UserJob.AspNetUser_Id == id
        //                select UserJob;
        //    foreach (var job in uJobs)
        //    {
        //        _list.Add(job);
        //    }
        //    return _list;
        //}

        //public List<UserJob> getJobsByTechId(int id)
        //{
        //    List<UserJob> _list = new List<UserJob>();
        //    var jobs = from UserJob in db.UserJobs
        //               where UserJob.Technician_Id == id
        //               select UserJob;
        //    foreach(var job in jobs)
        //    {
        //        _list.Add(job);
        //    }
        //    return _list;
        //}

        //public int getTechIdByUserId(string id)
        //{
        //    var techid = (from Technician in db.Technicians
        //                  where Technician.AspNetUser_Id == id
        //                  select Technician.Id).FirstOrDefault();
        //    return techid;
        //}

        //public List<UserJob> getTechJobsByTechId(int id)
        //{
        //    List<UserJob> _list = new List<UserJob>();
        //    var tJobs = from UserJob in db.UserJobs
        //                where UserJob.Technician_Id == id
        //                select UserJob;
        //    foreach (var job in tJobs)
        //    {
        //        _list.Add(job);
        //    }
        //    return _list;
        //}

        //public List<Proposal> getProposalByJobId(int id)
        //{
        //    List<Proposal> _list = new List<Proposal>();
        //    var jobProps = from Proposal in db.Proposals
        //                   where Proposal.UserJob_Id == id
        //                   select Proposal;
        //    foreach(var prop in jobProps)
        //    {
        //        _list.Add(prop);
        //    }
        //    return _list;
        //}

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }

        public bool isNullOrEmpty(String str)
        {
            if (str == null || str.Equals(""))
            {
                return true;
            }
            return false;
        }
    }
}
