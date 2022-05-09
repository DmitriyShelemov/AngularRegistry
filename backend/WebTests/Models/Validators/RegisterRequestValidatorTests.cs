using NUnit.Framework;
using System.Linq;

namespace Web.Models.Validators.Tests
{
    public class RegisterRequestValidatorTests
    {
        private RegisterRequestValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new RegisterRequestValidator();
        }

        [Test]
        public void Validate_Pass()
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = "Some_comple][_p@$$w0rd"
            };

            var result = _validator.Validate(req);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Validate_Pass_WithoutProvince()
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                Agreed = true,
                Password = "Some_comple][_p@$$w0rd"
            };

            var result = _validator.Validate(req);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Validate_NotAreed()
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25,
                Agreed = false,
                Password = "1W"
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("Please agree to work for food.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Validate_EmptyEmail(string email)
        {
            var req = new RegisterRequest
            {
                Email = email,
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = "1W"
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("'Email' must not be empty.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase("sometestmail.com")]
        [TestCase("sometest@mailcom")]
        [TestCase("@mail.com")]
        [TestCase("sometest@")]
        public void Validate_InvalidEmail(string email)
        {
            var req = new RegisterRequest
            {
                Email = email,
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = "1W"
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("Email must be a valid one.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Validate_EmptyPassword(string pwd)
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = pwd
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("'Password' must not be empty.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase("1word")]
        [TestCase("Word")]
        public void Validate_InvalidPassword(string pwd)
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 5,
                ProvinceId = 25,
                Agreed = true,
                Password = pwd
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("Password must contain min 1 digit and min 1 uppercase letter.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Validate_InvalidCountry(int id)
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = id,
                ProvinceId = 25,
                Agreed = true,
                Password = "1W"
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("Country is a required field.", result.Errors.FirstOrDefault().ErrorMessage);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Validate_InvalidProvince(int id)
        {
            var req = new RegisterRequest
            {
                Email = "sometest@mail.com",
                CountryId = 1,
                ProvinceId = id,
                Agreed = true,
                Password = "1W"
            };

            var result = _validator.Validate(req);

            Assert.IsFalse(result.IsValid);
            Assert.IsNotNull(result.Errors.FirstOrDefault());
            Assert.AreEqual("Province is a required field.", result.Errors.FirstOrDefault().ErrorMessage);
        }
    }
}