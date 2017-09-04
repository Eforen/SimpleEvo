using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Tests
{
    [TestClass()]
    public class FloatExtensionTests
    {
        [TestMethod()]
        public void RemapTest()
        {
            float f = 40;
            Assert.AreEqual(1f, f.Remap(0, 40, 0, 1));
        }

        [TestMethod()]
        public void RemapTest2()
        {
            float f = 40;
            Assert.AreEqual(100f, f.Remap(0, 40, 0, 100));
        }
    }
}