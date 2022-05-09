using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;
using System;
using System.Security.Cryptography;

namespace Bl.Tests
{
    [TestFixture]
    public class PasswordServiceTests
    {
        private PasswordService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new PasswordService();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void CalculateHashTest_PwdRequired(string pwd)
        {
            // Arrange

            // Act
            TestDelegate act = () => _service.CalculateHash(pwd);


            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Test]
        public void CalculateHashTest_Hashed()
        {
            // Arrange
            const string pwd = "Some_comple][_p@$$w0rd";

            // Act
            var hashed = _service.CalculateHash(pwd);

            // Assert
            Assert.IsTrue(hashed != pwd);
        }
    }
}