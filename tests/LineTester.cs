using Labyrinth.Composition;
using Labyrinth.Composition.Interfaces;

using NuGet.Frameworks;

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
        bool? bas;

        private IEditableLine CLine => CreateLine(start, end, Comparer, bas);

        private static Line CreateLine(
            int? start,
            int? End,
            IComparer<ILine>? line,
            bool? bas
            )
        {
            if (bas is not null && (bool)bas)
            {
                return new Line(null,
                    new SimpleFactory<ICompareLineByStart<ILine>>(
                        () => {
                            return new ComparebyStart();
                        }));
            }
            return new Line(
                ((start is null) ? 5 : (int)start),
                ((End is null) ? 10 : (int)End),
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
                CLine.CompareTo(CreateLine(4, null, null, null)) > 0);
        }
        [TestMethod]
        public void TestLenComparison()
        {
            this.Comparer = new CompareByLen();
            Assert.IsFalse(CLine.CompareTo(CreateLine(4, 15, null, null)) > 0);
        }
        [TestMethod]
        public void TestRedefinedComparison()
        {
            CLine.CompareBy = new CompareByLen();
            Assert.IsFalse(CLine.CompareTo(CreateLine(4, 15, null, null)) > 0);
        }
        [TestMethod]
        [DataRow(8)]
        [DataRow(-2)]
        [DataRow(-5)]
        [DataRow(3)]
        public void TestLenAdjustement(int row)
        {
            var a = this.CLine;
            var b = this.CLine;
            b.AdjustLen(row, 'c', 89);
            Assert.AreNotEqual(a.Len, b.Len, row);
        }
        [TestMethod]
        [DataRow(8)]
        [DataRow(-2)]
        [DataRow(-5)]
        [DataRow(3)]
        public void TestEndAfterStartAdjustement(int row)
        {
            var a = this.CLine;
            var b = this.CLine;
            a.AdjustStart(row, 'c', 980);
            Assert.IsTrue(a.LineStart != b.LineStart);
            Assert.IsTrue(a.LineEnd != b.LineEnd);
            Assert.IsTrue(b.LineEnd == a.LineEnd + row);
        }
        [TestMethod]
        [DataRow(8)]
        [DataRow(-2)]
        [DataRow(-5)]
        [DataRow(3)]
        public void TestEndAfterLenAdjustement(int row)
        {
            var a = this.CLine;
            var b = this.CLine;
            a.AdjustLen(row, 'c', 98);
            Assert.IsTrue(a.LineEnd != b.LineEnd);
            Assert.IsTrue(b.LineEnd == a.LineEnd + row);
            Assert.AreEqual(a.Len, b.Len, row);
        }
        [TestMethod]
        [DataRow(-5)]
        [DataRow(-50)]
        public void TestSafeAdjustement(int row)
        {
            var a = this.CLine;
            a.AdjustLen(row, 'U', 10);
            Assert.IsFalse(a.AdjustLen(row, 'L', 8));
        }
        [TestMethod]
        public void TestEnd()
        {
            var a = CreateLine(null, null, Comparer, bas);
            Assert.IsTrue(a.LineEnd >= a.LineStart);
        }
    }
}
