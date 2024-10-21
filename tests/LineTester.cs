using Labyrinth.Composition;
using Labyrinth.Composition.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    [TestClass]
    public class LineTester
    {
        IComparer<ILine>? Comparer { get; set; }
        int? start;
        int? end;

        private ILine CLine => CreateLine(start, end, Comparer);

        private ILine CreateLine(
            int? start,
            int? End,
            IComparer<ILine>? line
            )
        {
            return new Line(
                ((start is null) ?  5  : (int)start),
                ((End is null)   ?  10 : (int)End),
                'C',
                8,
                line,
                new SimpleFactory<ICompareLineByStart<ILine>>(
                () => {
                    return new ComparebyStart(); 
                }));
        }
        [TestMethod]
        public void TestBaseComparison()
        {
            Assert.IsTrue(
                CLine.CompareTo(CreateLine(4, null, null))> 0);
        }
        [TestMethod]
        public void TestLenComparison()
        {
            this.Comparer = new CompareByLen();
            Assert.IsFalse(CLine.CompareTo(CreateLine(4, 15, null)) > 0);
        }
        [TestMethod]
        public void TestRedefinedComparison()
        {
        }
    }
}
