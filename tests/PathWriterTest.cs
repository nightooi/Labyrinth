using Labyrinth.Composition;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace tests
{
    [TestClass]
    public class PathWriterTest
    {
        private PathWriter Writer => CreateWriter();
        private PathWriter CreateWriter()
        {
            return new SimpleFactory<PathWriter>(() =>
            {
                return new PathWriter(new SimpleFactory<GeneralPadding>(() =>
                {
                    return new GeneralPadding()
                    {
                        PaddingX = string.Empty,
                        PaddingY = string.Empty
                    };
                }), new SimpleFactory<Characters>(() =>
                {
                    return new Characters()
                    {
                        Down = 'D',
                        Up = 'U',
                        Right = 'R',
                        Left = 'L',
                        Space = ' ',
                        NewLine = '\n'
                    };
                }));
            }).Create();
        }
        [TestMethod]
        public void TestInsertUP()
        {
            ISimpleFactory<PathWriter> j = new SimpleFactory<PathWriter>(() =>
            {
                return new PathWriter(new SimpleFactory<GeneralPadding>(() =>
                {
                    return new GeneralPadding()
                    {
                        PaddingX = string.Empty,
                        PaddingY = string.Empty
                    };
                }), new SimpleFactory<Characters>(() =>
                {
                    return new Characters()
                    {
                        Down = 'D',
                        Up = 'U',
                        Right = 'R',
                        Left = 'L',
                        Space = ' ',
                        NewLine = '\n'
                    };
                }));
            });
            var a = j.Create();
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            b = a.InsertDown(5, b);
            b = a.InsertUp(2, b);
            var c = b.ToString();
            Console.WriteLine(c);
            var k = c.Split("\r\n");
            Assert.IsTrue(k[2] == "D");
            Assert.IsTrue(k[3] == "U");
        }
        [TestMethod]
        public void TestInsertUpAtZero()
        {
            var write = Writer;
            StringBuilder builder = new();
            builder = write.InsertUp(8, builder);
            builder.Append("\r\n").Append("\r\n");
            var a = builder.ToString();
            Assert.IsTrue(a.Length == 4);
            Assert.IsFalse(a.Contains("U"));
        }
        [TestMethod]
        [DataRow(8)]
        [DataRow(16)]
        public void TestInsertRight(int value)
        {
            var write = Writer;
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            Assert.IsTrue(b[0] == '\r' && b[1] == '\n');
            Console.WriteLine(b.ToString() + " :::::::OUTPUTBEFORE");
            b = write.InsertRight(value, b);
            b = write.InsertRight(value, b);
            Console.WriteLine(b.ToString());
            Console.WriteLine(b.Length + "::::Len");
            Console.WriteLine(b[0] + " ::::First");
            Assert.IsTrue(b[0] == '\r');
            Assert.IsTrue(b.ToString().StartsWith("\r\n"));
            Assert.IsTrue(b.ToString().EndsWith("\r\n"));
            Assert.IsTrue(b.ToString().Contains('R'));
            Assert.IsTrue(b.ToString().Contains(
                "RRRRRRRRRRRRRRRR"));
            Assert.IsTrue(b.Length == (2 * value) + 4);
        }
        [TestMethod]
        public void TestLeft()
        {
            var write = Writer;
            StringBuilder field = new StringBuilder().Append("\r\n").Append("\r\n");
            field = write.InsertRight(8, field);
            field = write.InsertDown(8, field);
            field = write.InsertLeft(8, field);
            var b = field.ToString().Split("\r\n");
            Console.Write(field.ToString() + "::::InTest");
            Console.WriteLine(field.Length);
            Assert.IsTrue(field.ToString().StartsWith("\r\n"));
            Assert.IsTrue(field.ToString().EndsWith("\r\n"));
            Assert.IsTrue(field.Length == (10 * 8) + 2);
            Assert.IsTrue(b[8].Contains("L"));
            Assert.IsTrue(b[8] == "LLLLLLLL");
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(9)]
        [DataRow(7)]
        [DataRow(5)]
        [DataRow(33)]
        [DataRow(8)]
        [DataRow(10)]
        public void TestLeftAtZero(int value)
        {
            var write = Writer;
            StringBuilder field = new StringBuilder().Append("\r\n").Append("\r\n");
            field = write.InsertLeft(value, field);
            Console.WriteLine(field.ToString() + "::::InTest");
            Console.WriteLine(field.Length);
            Assert.IsTrue(field.Length == 4);
            Assert.IsFalse(field.ToString().Contains("L"));
        }
        [TestMethod]
        public void TestLeftVsRight()
        {
            var write = Writer;
            StringBuilder field = new StringBuilder().Append("\r\n").Append("\r\n");
            field = write.InsertRight(8, field);
            field = write.InsertLeft(8, field);
            var a = field.ToString();
            Console.WriteLine(a);
            Assert.IsFalse(a.Contains("R"));
            Assert.IsFalse(a.Contains("L"));
            Assert.IsTrue(a.Length == 4);
            Assert.IsTrue(a.Contains("\r\n"));
        }
        [TestMethod]
        [DataRow(7)]
        public void TestConsecutiveDown(int val)
        {
            var writer = Writer;
            StringBuilder builder = new();
            builder.Append("\r\n");
            builder.Append("\r\n");
            for (int i = val; i > 0; i--)
            {
                builder = writer.InsertRight(8, builder);
                builder = writer.InsertDown(8, builder);
                //Console.WriteLine(builder.ToString());
            }
            for (int i = val; i > 0; i--)
            {
                builder = writer.InsertLeft(8, builder);
                builder = writer.InsertDown(8, builder);
            }
            var c = builder.ToString();
            Console.WriteLine(c);
        }
        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void TestDown(int value)
        {
            var a = Writer;
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            b = a.InsertDown(value, b);
            var c = b.ToString();
            var k = c.Split("\r\n");
            Console.WriteLine("Len::: {0}", b.Length);
            Console.WriteLine(c);
            Console.Write(k[value] + "::::value");
            Assert.IsTrue(c.Length == (3 * value) + 1);
            Assert.IsTrue(c.StartsWith("\r\n"));
            Assert.IsTrue(c.EndsWith("\r\n"));
            Assert.IsTrue(k[value].Contains("D"));
            Assert.IsTrue(k[value] == "D");
        }
        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void TestLeftDown(int value)
        {
            var a = Writer;
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            b = a.InsertLeft(value, b);
            b = a.InsertDown(value, b);
            var c = b.ToString();
            var k = c.Split("\r\n");
            Console.WriteLine(c);
            Console.WriteLine("Len::: {0}", b.Length);
            Assert.IsFalse(k[0].Contains("D"));
            Assert.IsTrue(c.Length == (3 * value) + 4);
            Assert.IsTrue(k[value].Contains("D"));
            Assert.IsTrue(k[value] == "D");
        }
        [TestMethod]
        public void TestSquare()
        {
            var a = Writer;
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            b = a.InsertRight(8, b);
            b = a.InsertDown(8, b);
            b = a.InsertLeft(8, b);
            b = a.InsertUp(8, b);
            b = a.InsertRight(8, b);
            b = a.InsertRight(8, b);
            b = a.InsertDown(8, b);
            b = a.InsertUp(8, b);


            Console.WriteLine(b.ToString());
            var c = b.ToString().Split("\r\n");
            Assert.IsTrue(c[1].Contains("RRRRRRRRRRRRRRRR"));
            Assert.IsTrue(c[2].Contains("U      D       D"));
            Assert.IsTrue(c[9].Contains("LLLLLLLL       R"));
        }
        [TestMethod]
        public void TestSquareTwo()
        {
            var a = Writer;
            StringBuilder b = new StringBuilder();
            b.Append("\r\n").Append("\r\n");
            b = a.InsertDown(8, b);
            b = a.InsertRight(16, b);
            b = a.InsertUp(8, b);
            b = a.InsertRight(8, b);
            b = a.InsertRight(8, b);
            b = a.InsertDown(8, b);
                
            Console.WriteLine(b.ToString());
            var c = b.ToString().Split("\r\n");
            Assert.IsTrue(c[1].Contains("RRRRRRRRRRRRRRRR"));
            Assert.IsTrue(c[2].Contains("U      D       D"));
            Assert.IsTrue(c[9].Contains("LLLLLLLL       R"));
        }
    }
}