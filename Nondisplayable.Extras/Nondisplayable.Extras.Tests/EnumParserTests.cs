using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nondisplayable.Extras.Tests
{
    [TestClass]
    public class EnumParserTests
    {
        [TestMethod]
        public void BasicParseWorks()
        {
            Assert.AreEqual(IceCreamFlavour.Picante, EnumParser.Parse<IceCreamFlavour>("Picante"));
        }

        [TestMethod]
        public void SloppySpacingWorks()
        {
            Assert.AreEqual(IceCreamFlavour.Picante, EnumParser.Parse<IceCreamFlavour>("  Picante   "));
        }

        [TestMethod]
        public void ParseCaseInsensitive()
        {
            Assert.AreEqual(IceCreamFlavour.Picante, EnumParser.ParseCaseInsensitive<IceCreamFlavour>("picante"));
            Assert.AreEqual(IceCreamFlavour.Picante, EnumParser.ParseCaseInsensitive<IceCreamFlavour>("piCANte"));
            Assert.AreEqual(IceCreamFlavour.Picante, EnumParser.ParseCaseInsensitive<IceCreamFlavour>("  picante   "));
        }

        [TestMethod]
        public void BruteForceTest()
        {
            var names = Enum.GetNames(typeof(IceCreamFlavour));
            foreach (var name in names)
            {
                var parsed = EnumParser.Parse<IceCreamFlavour>(name);
                Assert.AreEqual(Enum.Parse(typeof(IceCreamFlavour), name), parsed);
            }
        }

        [TestMethod]
        public void BadValuesFail()
        {
            try
            {
                var value = EnumParser.Parse<IceCreamFlavour>("MuchoPicante");
                Assert.Fail("Exception not thrown");
            }
            catch (EnumParseException e)
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.ValidNames);
                Assert.IsNotNull(e.InvalidName);
                Assert.AreEqual("MuchoPicante", e.InvalidName);
                Assert.AreEqual(4, e.ValidNames.Count);
                Assert.IsTrue(e.ValidNames.ToList().Contains("Sherbert"));
            }
            catch
            {
                Assert.Fail("Wrong exception type");
            }
        }

        enum IceCreamFlavour
        {
            Sherbert,
            Orange,
            Strawberry,
            Picante
        }
    }
}
