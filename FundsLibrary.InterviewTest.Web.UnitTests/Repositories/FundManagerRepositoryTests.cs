using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpectedObjects;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Repositories
{
    public class FundManagerRepositoryTests
    {
        [Test]
        public async Task ShouldGetAll()
        {
            //Arrange
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            IEnumerable<FundManager> fundManagers = new[] { new FundManager() };
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/"))
                .Returns(Task.FromResult(fundManagers))
                .Verifiable();
            var repository = new FundManagerRepository(mockServiceClient.Object);

            //Act
            var result = await repository.GetAll();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            mockServiceClient.Verify();
        }

        [Test]
        public async Task ShouldGet()
        {
            //Arrange
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var fundManager = new FundManager();
            var guid = Guid.NewGuid();
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + guid))
                .Returns(Task.FromResult(fundManager))
                .Verifiable();
            var repository = new FundManagerRepository(mockServiceClient.Object);

            // Don't reuse the fundManager object, it may have mutated due to the act.
            // ExpectedObject lib compares the properties via eqality instead of the object.
            var expectedFundManager = new FundManager().ToExpectedObject();

            //Act
            var actualFundManager = await repository.Get(guid);

            //Assert
            Assert.That(actualFundManager, Is.Not.Null);
            Assert.That(actualFundManager, Is.EqualTo(expectedFundManager));
            mockServiceClient.Verify();
        }
    }
}
