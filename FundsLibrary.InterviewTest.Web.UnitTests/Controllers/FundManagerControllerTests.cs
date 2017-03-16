using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ExpectedObjects;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Controllers;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Controllers
{
    public class FundManagerControllerTests
    {
        [Test]
        public async Task ShouldGetIndexPage()
        {
            //Arrange
            var mockFundManRepo = new Mock<IFundManagerRepository>();
            IEnumerable<FundManager> fundManagerModels = new FundManager[0];
            mockFundManRepo.Setup(m => m.GetAll())
                .Returns(Task.FromResult(fundManagerModels))
                .Verifiable();
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m=>m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mockFundManRepo.Object, mockFundRepo.Object);

            //NUnit-IS.EqualTo compares single dimensional arrays, so this will match.
            var expected = new FundManager[0];

            //Act
            var actual = await controller.Index();

            //Assert
            Assert.That(actual, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult)actual).Model, Is.EqualTo(expected));
            mockFundManRepo.Verify();
        }

        [Test]
        public async Task ShouldGetDetailsPage()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.Setup(m => m.Get(guid))
                .Returns(Task.FromResult(fundManagerModel))
                .Verifiable();
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m => m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mock.Object, mockFundRepo.Object);

            var expected = new FundManager { Funds = new List<Fund>() }.ToExpectedObject();

            //Act
            var actual = await controller.Details(guid);

            //Assert
            Assert.That(actual, Is.TypeOf<ViewResult>());
            Assert.That(((ViewResult)actual).Model, Is.EqualTo(expected));
        }

        [Test]
        public async Task ShouldGetEditPage()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(guid)).Returns(Task.FromResult(fundManagerModel));
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m => m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mock.Object, mockFundRepo.Object);
            var expected = new FundManager().ToExpectedObject();

            //Act
            var actual = await controller.Edit(guid);

            //Assert
            Assert.That(actual, Is.TypeOf<ViewResult>());
            mock.Verify();
            Assert.That(((ViewResult)actual).Model, Is.EqualTo(expected));
        }

        [Test]
        public async Task ShouldGetRedirectedToErrorFromEditPageIfNullGuid()
        {
            //Arrange
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(validGuid)).Returns(Task.FromResult(fundManagerModel));
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m => m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mock.Object, mockFundRepo.Object);

            //Act
            var actual = await controller.Edit((Guid?)null);

            //Assert
            Assert.That(actual, Is.TypeOf<RedirectToRouteResult>());
            var redir = (RedirectToRouteResult)actual;
            Assert.That("errorMessage", Is.EqualTo(redir.RouteValues.Keys.First()));
            mock.Verify();
        }

        [Test]
        public async Task ShouldGetIndexPageIfSuccessfulDelete()
        {
            //Arrange
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            mock.SetupAllProperties();
            mock.Setup(m => m.Delete(validGuid)).Returns(Task.FromResult(true));
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m => m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mock.Object, mockFundRepo.Object);

            //Act
            var actual = await controller.Delete(validGuid);


            //Assert
            Assert.That(actual, Is.TypeOf<RedirectToRouteResult>());
            var redir = (RedirectToRouteResult)actual;
            Assert.That("Index", Is.EqualTo(redir.RouteValues.Values.First()));
            mock.Verify();
        }

        [Test]
        public async Task ShouldGetRedirectedToErrorFromDeletePageIfNullGuid()
        {
            //Arrange
            var validGuid = Guid.NewGuid();
            var mock = new Mock<IFundManagerRepository>();
            var fundManagerModel = new FundManager();
            mock.SetupAllProperties();
            mock.Setup(m => m.Get(validGuid)).Returns(Task.FromResult(fundManagerModel));
            var mockFundRepo = new Mock<IFundRepository>();
            IEnumerable<Fund> emptyFunds = new List<Fund>();
            mockFundRepo.Setup(m => m.GetFunds(new Guid()))
                .Returns(Task.FromResult(emptyFunds))
                .Verifiable();
            var controller = new FundManagerController(mock.Object, mockFundRepo.Object);

            //Act
            var actual = await controller.Delete(null);

            //Assert
            Assert.That(actual, Is.TypeOf<RedirectToRouteResult>());
            var redir = (RedirectToRouteResult)actual;
            Assert.That("errorMessage", Is.EqualTo(redir.RouteValues.Keys.First()));
            mock.Verify();
        }
    }
}
