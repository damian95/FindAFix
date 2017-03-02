using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindAFixMVC.Controllers;
using FindAFixMVCTest.Models;

namespace FindAFixMVCTest.Controllers
{
    [TestClass]
    public class UnitTest_HomeController
    {
        [TestMethod]
        public void TestMethod_TechHome()
        {
            //arrange
            HomeController controller = new HomeController(new FakeHomeRepository());
            //act
            System.Web.Mvc.ViewResult result = controller.TechnicianHome() as System.Web.Mvc.ViewResult;
            //assert
            Assert.IsNotNull(result);
        }
    }
}
