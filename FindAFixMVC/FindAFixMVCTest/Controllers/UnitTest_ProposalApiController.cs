using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindAFixMVC.Controllers;
using FindAFixMVCTest.Models;
using FindAFixMVC.Models.DBContext;
using System.Web.Http.Results;

namespace FindAFixMVCTest
{
    [TestClass]
    public class UnitTest_ProposalApiController
    {
        [TestMethod]
        public void ProposalApiController_GetOne()
        {
            //Arrange
            ProposalsApiController controller = new ProposalsApiController(new FakeProposalsApiRepository());
            //Act
            OkNegotiatedContentResult<Proposal> result = controller.GetProposal(1) as OkNegotiatedContentResult<Proposal>;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
        }
    }
}
