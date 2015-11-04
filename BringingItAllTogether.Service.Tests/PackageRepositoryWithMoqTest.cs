using System;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.Data;
using BringingItAllTogether.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BringingItAllTogether.Service.Tests
{
    [TestClass]
    public class PackageRepositoryWithMoqTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_When_Save_With_Invalid_Expiry_Date()
        {
            var package = new Package()
            {
                Title = "testsers",
                Description = "testers",
                ModifiedDate = DateTime.Now.AddDays(-5)
            };

            var repositoryMock = new Mock<IRepository<Package>>();
            repositoryMock.Setup(x => x.Insert(package));

            var repository = new PackageService(repositoryMock.Object);

            repository.InsertPackage(package);
        }

        [TestMethod]
        public void Test_When_Save_With_valid_Expiry_Date()
        {
            var package = new Package()
            {
                Title = "testsers",
                Description = "testers",
                Location = "stafford",
                ModifiedDate = DateTime.Now.AddDays(10)
            };

            var repositoryMock = new Mock<IRepository<Package>>();
            repositoryMock.Setup(x => x.Insert(package));

            var repository = new PackageService(repositoryMock.Object);

            repository.InsertPackage(package);

            repositoryMock.Verify(x => x.Insert(package), Times.Once);
        }
    }
}
