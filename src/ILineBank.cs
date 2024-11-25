using Microsoft.Extensions.Options;

using System.Collections;
using System.Linq.Expressions;
using System.Numerics;

namespace Labyrinth.Composition.Interfaces;

public interface ILineBank<T>  where T: ILine
{
    IReadOnlyCollection<ILine> Bank { get; }
    bool AppendToLine(T Line, int len, char c, int lastIns);
    bool AppendToLine(int LineNumber, int len, char c, int lastIns);
    bool AdjustLine(int LineNumber, int len, char c, int lastIns);
    bool GetLine(int LineNumber, out T? item, int lastIns);
    ref T GetFirst();
    ref T GetLast();
    ref T? GetLastest();
    bool Remove(T item);
    bool Remove(int item);
}
