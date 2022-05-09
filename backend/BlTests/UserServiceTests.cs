using NUnit.Framework;
using System;
using Moq;
using Abstractions;
using System.Threading.Tasks;
using Domain;
using Infrastructure;

namespace Bl.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _repository;
        private Mock<IProvinceService> _provinceService;
        private Mock<IPasswordService> _passwordService;
        private UserService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IUserRepository>();
            _provinceService = new Mock<IProvinceService>();
            _passwordService = new Mock<IPasswordService>();
            _service = new UserService(_repository.Object, _provinceService.Object, _passwordService.Object);
        }

        [Test]
        public async Task RegisterTest_NullAsync()
        {
            // Arrange
            // Act
            // Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.RegisterAsync(null));
        }

        [Test]
        public async Task RegisterTest_AlreadyExistAsync()
        {
            // Arrange
            var user = new User
            {
                Email = "sometest@mail.com"
            };

            _repository.Setup(x => x.ExistsAsync(user.Email)).ReturnsAsync(true);

            // Act
            // Assert
            var ex = Assert.ThrowsAsync<UserFriendlyException>(async () => await _service.RegisterAsync(user));
            Assert.AreEqual($"'{user.Email}' is already registered.", ex.Message);
        }

        [Test]
        public async Task RegisterTest_ProvinceIsMissingAsync()
        {
            // Arrange
            var user = new User
            {
                Email = "sometest@mail.com",
                CountryId = 5
            };

            _repository.Setup(x => x.ExistsAsync(user.Email)).ReturnsAsync(false);
            _provinceService.Setup(x => x.HasProvincesAsync(user.CountryId)).ReturnsAsync(true);

            // Act
            // Assert
            var ex = Assert.ThrowsAsync<UserFriendlyException>(async () => await _service.RegisterAsync(user));
            Assert.AreEqual("Please specify Province field.", ex.Message);
        }

        [Test]
        public async Task RegisterTest_ProvinceNotFromCountryAsync()
        {
            // Arrange
            var user = new User
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25
            };

            _repository.Setup(x => x.ExistsAsync(user.Email)).ReturnsAsync(false);
            _provinceService.Setup(x => x.ExistsAsync(user.CountryId, user.ProvinceId.Value)).ReturnsAsync(false);

            // Act
            // Assert
            var ex = Assert.ThrowsAsync<UserFriendlyException>(async () => await _service.RegisterAsync(user));
            Assert.AreEqual("Specified Province does not belong provided Country.", ex.Message);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task RegisterTest_PassAsync(int queryReturn)
        {
            // Arrange
            const string pwd = "Some_comple][_p@$$w0rd";
            const string pwdHash = "hash";
            var user = new User
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = pwd
            };

            _repository.Setup(x => x.ExistsAsync(user.Email)).ReturnsAsync(false);
            _repository.Setup(x => x.RegisterAsync(user)).ReturnsAsync(queryReturn);
            _provinceService.Setup(x => x.ExistsAsync(user.CountryId, user.ProvinceId.Value)).ReturnsAsync(true);
            _passwordService.Setup(x => x.CalculateHash(user.Password)).Returns(pwdHash);

            // Act
            var result = await _service.RegisterAsync(user);

            // Assert
            Assert.AreEqual(pwdHash, user.Password);
            Assert.AreEqual(queryReturn > 0, result);
            _passwordService.Verify(x => x.CalculateHash(pwd), Times.Once);
            _repository.Verify(x => x.RegisterAsync(user), Times.Once);
        }
    }
}