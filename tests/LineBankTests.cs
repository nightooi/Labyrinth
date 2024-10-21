using Labyrinth.Composition.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    // I kinda need to test the DI system iteself to make sure
    // copies aren't being sent around but actuall new instantiations..
    [TestClass]
    public class LineBankTests
    {
        private ILineBank Bank => CreateBank();

        private ILineBank CreateBank()
        {
            return new SimpleFactory<ILineBank>(() =>
            {
                return new LineBank();
            }).Create();
        }
        [TestMethod]
        public void TestEnumeration()
        {
            
        }
        [TestMethod]
        public void TestListFunction()
        {
            
        }
    }
}
