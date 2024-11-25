using Labyrinth.Composition;
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
        private static ILineBank<ILine> Bank => CreateBank();

        private static ILineBank<ILine> CreateBank()
        {
            var b = new object[1];
            b[0] = new SimpleFactory<CompareByLen>(() => {
                return new CompareByLen(); });
            var a = new ParamFactory<ILine>((b) => {
                return new Line(null,
                    (ISimpleFactory<ICompareLineByStart<ILine>>)b[0]);
            });
            return new SimpleFactory<ILineBank<ILine>>(() =>
            {
                return new LineBank(a, 2);
            }).Create();
        }
        [TestMethod]
        public void TestIncrease()
        {
            var a = Bank;
            int k = a.Bank.Count;
            a.AdjustLine(1, 8, 'c', 8);
            Assert.IsTrue(k < a.Bank.Count);
        }
        [TestMethod]
        [DataRow(4)]
        [DataRow(40)]
        [DataRow(50)]
        [DataRow(60)]
        [DataRow(80)]
        public void TestCascadingChanges(int row)
        {
            var a = Bank;
            for(int i = 0; i < row; i++)
            {
                a.AdjustLine(i, 8, '8', 8);
            }
            //Assert.IsTrue();
        }
    }
}
