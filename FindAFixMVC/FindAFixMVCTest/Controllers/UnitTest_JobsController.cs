using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindAFixMVC.Controllers;
using FindAFixMVCTest.Models;
using FindAFixMVC.Models.DBContext;
using FindAFixMVC.Models.ViewModels;
using System.Collections.Generic;

namespace FindAFixMVCTest
{
    [TestClass]
    public class UnitTest_JobsController
    {
        [TestMethod]
        public void JobsController_Details()
        {
            //arrrange
            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.ViewResult result = controller.Details(1) as System.Web.Mvc.ViewResult;

            //assert
            Assert.IsNotNull(result);
            UserJob resultModel = result.Model as UserJob;
            Assert.AreEqual(1, resultModel.Id);
        }

        [TestMethod]
        public void JobsController_DetailsPost()
        {
            //arrange
            Proposal model = new Proposal()
            {
                Id = 1,
                PriceEst = 2.20M,
                Message = "a",
                AvailabilityDate = DateTime.Now,
                UserJob_Id = 1,
                Technician_Id = 1,
                UserSeen = false,
                TechSeen = false,
                Technician = new Technician(),
                UserJob = new UserJob()
            };

            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.RedirectToRouteResult result = controller.Details(model) as System.Web.Mvc.RedirectToRouteResult;

            //assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void JobsController_CreatePost()
        {
            //arrange
            JobViewModel _model = new JobViewModel()
            {
                Id = 1,
                JobTitle = "title",
                JobDecription = "description",
                JobType = "Fencing",
                Latitude = 2.21287821,
                Longitude = 1.111111
            };
            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.RedirectToRouteResult result = controller.Create(_model) as System.Web.Mvc.RedirectToRouteResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void JobsController_AvailableJobsPost()
        {
            //arrange
            Proposal _model = new Proposal()
            {
                Id = 1,
                PriceEst = 2.22M,
                Message = "message",
                AvailabilityDate = DateTime.Now,
                UserJob_Id = 1,
                Technician_Id = 1,
                UserSeen = false,
                TechSeen = false
            };

            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.ViewResult result = controller.AvailableJobs(_model) as System.Web.Mvc.ViewResult;

            //assert
            Assert.IsNotNull(result);

            List<UserJob> resultModel = result.Model as List<UserJob>;
            Assert.AreEqual(0, resultModel.Count);
            //Assert.IsInstanceOfType(resultModel, string);
        }

        [TestMethod]
        public void JobsController_Proposal()
        {
            //arrange
            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.ViewResult result = controller.Proposals(1) as System.Web.Mvc.ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void JobsController_AcceptProposal()
        {
            //arrange
            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.RedirectToRouteResult result = controller.AcceptProposal(1) as System.Web.Mvc.RedirectToRouteResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void JobsController_CategoryJobs()
        {
            //arrange
            JobsController controller = new JobsController(new FakeJobRepository());

            //act
            System.Web.Mvc.ViewResult result = controller.CategoryJobs(1) as System.Web.Mvc.ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

    }
}
