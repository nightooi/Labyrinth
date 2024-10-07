using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Labyrinth.Composition;

namespace tests
{
    [TestClass]
    public class EditorHistoryTests
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
        public void Restore()
        {
            var writer = this.Writer;

        }
        [TestMethod]
        public void LastInsert()
        {
            var writer = this.Writer;
            StringBuilder sb = new();
            sb = writer.InsertRight(8, sb);
            writer.LastInsertion();
        }
    }
}
