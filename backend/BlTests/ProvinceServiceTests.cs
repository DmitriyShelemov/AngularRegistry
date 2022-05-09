using NUnit.Framework;
using System;
using Moq;
using Abstractions;
using System.Threading.Tasks;

namespace Bl.Tests
{
    [TestFixture]
    public class ProvinceServiceTests
    {
        private Mock<IProvinceRepository> _repository;
        private ProvinceService _service;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IProvinceRepository>();
            _service = new ProvinceService(_repository.Object);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public async Task HasProvincesTest_CountryIdAsync(int id)
        {
            // Arrange

            // Act
            // Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _service.HasProvincesAsync(id));
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task HasProvincesTest_PassAsync(bool result)
        {
            // Arrange
            const int id = 1;
            _repository.Setup(x => x.HasAnyByCountryAsync(id)).ReturnsAsync(result);

            // Act
            var actual = await _service.HasProvincesAsync(id);

            // Assert
            Assert.AreEqual(result, actual);
            _repository.Verify(x => x.HasAnyByCountryAsync(id), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task ExistsTest_PassAsync(bool result)
        {
            // Arrange
            const int id = 1;
            const int cId = 1;
            _repository.Setup(x => x.ExistsAsync(cId, id)).ReturnsAsync(result);

            // Act
            var actual = await _service.ExistsAsync(cId, id);

            // Assert
            Assert.AreEqual(result, actual);
            _repository.Verify(x => x.ExistsAsync(cId, id), Times.Once);
        }
    }
}