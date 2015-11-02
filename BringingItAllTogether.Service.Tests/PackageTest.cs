using System;
using System.Data.Entity;
using BringingItAllTogether.Core.Data;
using BringingItAllTogether.Data;
using BringingItAllTogether.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BringingItAllTogether.Service;

namespace BringingItAllTogether.Service.Tests
{
    [TestClass]
    public class PackageTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_When_Save_With_Invalid_Date_Throw_Exception()
        {
            var service = new PackageService(new Repository<Package>(new IocDbContext()));

            var package = new Package()
            {
                Title = "testers",
                Description = "test12",
                Location = "stafford",
                ModifiedDate = DateTime.Now.AddDays(-5)
            };

            service.InsertPackage(package);
        }

        [TestMethod]
        public void Test_When_Save_with_Valid_Expiration_Save()
        {
            var service = new PackageService(new Repository<Package>(new IocDbContext()));
            var package = new Package()
            {
                Title = "test",
                Description = "test12",
                Location = "stafford",
                ModifiedDate = DateTime.Now.AddDays(10)
            };

            service.InsertPackage(package);
            Assert.IsNotNull(service.FindByTitle("test"));
        }
    }
}
