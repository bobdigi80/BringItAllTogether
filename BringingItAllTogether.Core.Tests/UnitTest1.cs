using System;
using BringingItAllTogether.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BringingItAllTogether.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var package = new Package() { Title = "TCS" };
            var mockRepo = new Mock<IPackage>();  



        }
    }
}
