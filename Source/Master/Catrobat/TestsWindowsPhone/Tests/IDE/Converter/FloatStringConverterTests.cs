﻿using Catrobat.IDEWindowsPhone.Converters;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.TestsWindowsPhone.Tests.IDE.Converter
{
    [TestClass]
    public class FloatStringConverterTests
    {
        [TestMethod]
        public void TestStringToFloatConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.ConvertBack((object)"4.2", null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is float);
            Assert.AreEqual((float)output, 4.2f);
        }

        [TestMethod]
        public void TestFloatToStringConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.Convert((object)4.2f, null, null, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is string);
            Assert.AreEqual((string)output, "4.2");
        }

        [TestMethod]
        public void TestFaultyStringToFloatConversion()
        {
            var conv = new FloatStringConverter();
            object output = conv.ConvertBack((object)"4d2", null, 42f, null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output is float);
            Assert.AreEqual((float)output, 42);
        }
    }
}