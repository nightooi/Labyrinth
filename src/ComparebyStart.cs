namespace Labyrinth.Composition.Interfaces;

public class ComparebyStart : ICompareLineByStart<ILine>
{
    public int Compare(ILine? x, ILine? y)
    {
        if (x is null && y is null) return 0;
        if (x is null && y is not null) return -1;
        if (x is not null && y is null) return 1;
        if (x! > y!) return 1;
        if (y! > x!) return -1;
        return 0;
    }
}













