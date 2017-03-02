using FindAFixMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAFixMVC.Models.DBContext;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Drawing;
using FindAFixMVC.Properties;
using FindAFixMVC.Models.ViewModels;
using FindAFixMVC.Models.Repositories;

namespace FindAFixMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HomeIRepository<Technician> repo;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController(HomeIRepository<Technician> _repo)
        {
            this.repo = _repo;
        }

        public HomeController() : this(new HomeRepository())
        {

        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult TechnicianHome()
        {
            //if this is the users first time logging in as a technician, create teachnician and technicianProfile in database
            //get the technicians user ID
            var uid = repo.getUserId();
            //var uid = User.Identity.GetUserId();

            if (TempData["techReg"] != null)
            {                
                //create instance of registerview model
                TechnicianRegisterViewModel trvm = new TechnicianRegisterViewModel();
                //pass it to a temp variable
                trvm = (TechnicianRegisterViewModel)TempData["techReg"];

                //creaet new technician and technician profile in database for suer
                //check to see if user has already been added
                var tech = repo.GetTechByUserId(uid);
                if (tech == null)
                {
                    //create technician from viewmodel and add to database
                    Technician model = new Technician()
                    {
                        CompanyName = trvm.CompanyName,
                        CEOFirstName = trvm.CEOFirstName,
                        CEOLastName = trvm.CEOLastName,
                        JobType = trvm.JobType,
                        AspNetUser_Id = uid,
                        ApplicationUser_Id = uid
                    };

                    repo.PostTech(model);
                }

                //get info to load onto page
                TechnicianProfile profile = new TechnicianProfile();

                //get default picture from resources and convert to Image object called defaultPic
                MemoryStream msNew = new MemoryStream();
                Image defaultPic = Resources.DefaultProfilePic;
                defaultPic.Save(msNew, System.Drawing.Imaging.ImageFormat.Jpeg);

                //find the technician currently logged in
                var newtech = repo.GetTechByUserId(uid);

                //add info to TechnicianProfile object to insert into database
                profile.techId = newtech.Id;
                profile.Description = "Add your company's description here";
                profile.Rating = "0";
                //convert Image object to byteArray
                profile.TechPicture = msNew.ToArray();

                //close stream and save TechnicianProfile to database
                msNew.Close();
                repo.PostTechProfile(profile);

                return View(profile);
            }else
            {
                //if is not the users first time logging in
                //get tech id of the current user logged in
                var techid = repo.GetTechIdByUserId(uid);
                //get the profile associated with the user
                var profile = repo.GetTechProfileByTechId(techid);

                return View(profile);
            }            
        }
    }
}