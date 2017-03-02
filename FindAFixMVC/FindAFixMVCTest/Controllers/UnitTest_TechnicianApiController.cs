using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindAFixMVC.Controllers;
using FindAFixMVCTest.Models;
using System.Web.Http.Results;
using FindAFixMVC.Models.DBContext;

namespace FindAFixMVCTest
{
    [TestClass]
    public class UnitTest_TechnicianApiController
    {
        [TestMethod]
        public void TechnicianApiController_GetOne()
        {
            //Arrange
            TechniciansApiController controller = new TechniciansApiController(new FakeTechnicianApiRepository());

            //Act
            OkNegotiatedContentResult<Technician> result = controller.GetTechnician(1) as OkNegotiatedContentResult<Technician>;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);

        }
    }
}
