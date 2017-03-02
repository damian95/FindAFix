using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindAFixMVC.Controllers;
using FindAFixMVCTest.Models;
using FindAFixMVC.Models.DBContext;
using System.Web.Http.Results;

namespace FindAFixMVCTest
{
    [TestClass]
    public class UnitTest_UserJobsApiController
    {
        [TestMethod]
        public void UserJobsApiController_GetOne()
        {
            //Arrange
            UserJobsApiController controller = new UserJobsApiController(new FakeUserJobsApiRepository());
            //Act
            OkNegotiatedContentResult<UserJob> result = controller.GetUserJob(1) as OkNegotiatedContentResult<UserJob>;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
