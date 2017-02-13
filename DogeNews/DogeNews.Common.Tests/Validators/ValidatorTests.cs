using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogeNews.Common.Validators;
using NUnit.Framework;

namespace DogeNews.Common.Tests.Validators
{
    [TestFixture]
    class ValidatorTests
    {
        [Test]
        public void ValidateThatObjectIsNotNull_ShouldThrowArgumentNullExceptionWithTheNameOfThePassedArgimentIfItIsNull()
        {
            string nullObj = null;
            var message = Assert.Throws<ArgumentNullException>(() => Validator.ValidateThatObjectIsNotNull(nullObj,nameof(nullObj)));
            Assert.AreEqual(message.ParamName, nameof(nullObj));
        }

        [Test]
        public void ValidateThatObjectIsNotNull_ShouldNotThrowWhenPassedArgumentIsNotNull()
        {
            var notNullObject = "#notnull";
            Assert.DoesNotThrow(() => Validator.ValidateThatObjectIsNotNull(notNullObject,nameof(notNullObject)));
        }

        [Test]
        public void ValidateThatStringIsNotNullOrEmpty_ShouldNotThrowWhenStringIsNotNullOrEmpty()
        {
            var notNullObj = "Not null or empty string.";
            Assert.DoesNotThrow(()=> Validator.ValidateThatStringIsNotNullOrEmpty(notNullObj,nameof(notNullObj)));
        }


        [TestCase(null)]
        [TestCase("")]
        public void ValidateThatStringIsNotNullOrEmpty_ShouldThrowArgumentNullExceptionWhenStringIsNullOrEmpty(string str)
        {
            var message = Assert.Throws<ArgumentNullException>(() => Validator.ValidateThatStringIsNotNullOrEmpty(str,nameof(str)));
            Assert.AreEqual(message.ParamName,nameof(str));
        }
    }
}
