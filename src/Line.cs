using Labyrinth.Composition.Interfaces;

namespace Labyrinth.Composition;

public class Line : ILine
{
    public int LineStart { get; set; }
    public int LineEnd { get; set; }
    public char LastInsert { get; set; }
    public IComparer<ILine> Comparer { get; set; }
    public int Len => LineEnd - LineStart;

    public int LastInsertPosition { get; set; }

    public Line(
        int Start,
        int End,
        char insert,
        int insertPosition,
        IComparer<ILine>? comparer,
        ISimpleFactory<ICompareLineByStart<ILine>> standardCompare)
    {
        LineStart = Start;
        LineEnd = End;
        LastInsert = insert;
        LastInsertPosition = insertPosition;
        Comparer = (comparer is null) ? standardCompare.Create() : comparer;
    }

    public int CompareTo(ILine? other)
    {
        return Comparer.Compare(this, other);
    }
}
