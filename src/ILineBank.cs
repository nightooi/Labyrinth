using Microsoft.Extensions.Options;

using System.Collections;
using System.Linq.Expressions;
using System.Numerics;

namespace Labyrinth.Composition.Interfaces;

public interface ILineBank<T>  where T: ILine
{
    LinkedList<T> Bank { get; }
    bool AppendtoLine(T Line, int len, char c);
    bool AdjustLine(int LineNumber, int len, char c);
    bool GetLine(int LineNumber, out T? item);
    bool GetFirst(out T? item);
    bool GetLast(out T? item);
    bool GetLastest(out T? item);
}

public abstract class LineBank : ILineBank<ILine>
{
    public LinkedList<ILine> Bank => throw new NotImplementedException();
    public bool AdjustLine(int LineNumber, int len, char c)
    {
        throw new NotImplementedException();
    }
    public bool AppendtoLine(ILine Line, int len, char c)
    {
        throw new NotImplementedException();
    }
    public bool GetFirst(out ILine? item)
    {
        throw new NotImplementedException();
    }
    public bool GetLast(out ILine? item)
    {
        throw new NotImplementedException();
    }
    public bool GetLastest(out ILine? item)
    {
        throw new NotImplementedException();
    }
    public bool GetLine(int LineNumber, out ILine? item)
    {
        throw new NotImplementedException();
    }
}
