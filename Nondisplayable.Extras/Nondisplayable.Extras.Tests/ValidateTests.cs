using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Nondisplayable.Extras.Tests
{
    [TestClass]
    public class ValidateTests
    {
        #region Object validation

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NullFails()
        {
            Validate.NotNull(null);
        }

        [TestMethod]
        public void NotNullPasses()
        {
            Validate.NotNull(string.Empty);
        }

        #endregion

        #region Type validation

        [TestMethod]
        public void WorksForBaseClasses()
        {
            Validate.IsOfType<Exception>(new ValidationException("Test"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WrongTypeFails()
        {
            Validate.IsOfType<StringBuilder>("Hello, World");
        }

        #endregion  

        #region String validation

        [TestMethod]
        public void FilledStringPasses()
        {
            var testString = "Hello, world";
            Validate.NotNullOrWhitespace(testString);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void EmptyStringFails()
        {
            Validate.NotNullOrWhitespace(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NullStringFails()
        {
            Validate.NotNullOrWhitespace(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WhitespaceStringFails()
        {
            Validate.NotNullOrWhitespace("                ");
        }

        #endregion
    }
}
