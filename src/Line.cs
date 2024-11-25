using Labyrinth.Composition.Interfaces;

using System.Runtime.CompilerServices;
using System.Transactions;

namespace Labyrinth.Composition;
public class Line : IEditableLine
{
    private enum ChangeLog { start, end }
    private List<int> startChangeLog = [];
    private List<int> EndChangeLog = [];
    int _linestart, _lineEnd, _lastInsertPostition;
    char _lastInsert;
    public int LineStart { 
        get {
            ExecuteChangeLog();
            return _linestart;
        } 
    }
    public int LineEnd { get
        {
            ExecuteChangeLog();
            return _lineEnd;
        }
    }
    public char LastInsert { get => _lastInsert; }
    
    public int Len => LineEnd - LineStart;
    public int LastInsertPosition { get => _lastInsertPostition; }
    public IComparer<ILine> CompareBy { get; set; }
    public Line(
        IComparer<ILine>? comparer,
        ISimpleFactory<ICompareLineByStart<ILine>> standardCompare)
    {
        CompareBy = (comparer is null) ? standardCompare.Create() : comparer;
    }
    public Line(
        int lineStart,
        int lineEnd,
        char lastIns,
        int insertPos,
        IComparer<ILine>? comparer,
        ISimpleFactory<ICompareLineByStart<ILine>> standardCompare)
    {
        _linestart = lineStart;
        _lineEnd = lineEnd;
        _lastInsert = lastIns;
        _lastInsertPostition = insertPos;
        CompareBy = (comparer is null) ? standardCompare.Create() : comparer;

    }
    public int CompareTo(ILine? other)
    {
        return this.CompareBy.Compare(this, other);
    }
    /// <summary>
    /// if len is negative and start - len is less than 0, returns false
    /// and does not perform operation.
    /// 
    /// </summary>
    /// <param name="len"> total len of insertion, negative or positive</param>
    /// <param name="c"> the character inserted</param>
    /// <param name="lastIns">the last position of the insertion</param>
    /// <returns></returns>
    public bool AdjustStart(int len, char c, int lastIns)
    {
        var res = LineStart + len;
        if (res < 0) return false;
        AppendChangelog(ChangeLog.start, len);
        _lastInsert = c;
        return true;
    }
    private void AppendChangelog(ChangeLog change, int val)
    {
        switch (change)
        {
            case ChangeLog.start:
                startChangeLog.Add(val);
                EndChangeLog.Add(val);
                break;
            case ChangeLog.end:
                EndChangeLog.Add(val);
                break;
        }
    }
    private void ExecuteChangeLog()
    {
        int resEnd = 0;
        int resStart = 0;
        var a = (startChangeLog.Count > EndChangeLog.Count) ? startChangeLog.Count : EndChangeLog.Count;
        for(int i =0; i < a; i++)
        {
           resEnd += (i < EndChangeLog.Count) ? EndChangeLog[i] : 0;
           resStart += (i < startChangeLog.Count) ? startChangeLog[i] : 0;
        }
        startChangeLog.Clear();
        EndChangeLog.Clear();

        _lineEnd += resEnd;
        _linestart += resStart;
    }
    ///<summary>
    /// if len is negative and start - len is less than 0, returns false
    /// and does not perform operation.
    /// </summary>
    /// <param name="len"> total len of insertion, negative or positive</param>
    /// <param name="c"> the character inserted</param>
    /// <param name="lastIns">the last position of the insertion</param>
    /// <returns></returns>
    public bool AdjustLen(int len, char c, int lastIns)
    {
        var res = LineStart + len;
        if (res < 0) return false;
        AppendChangelog(ChangeLog.end, len);
        _lastInsert = c;
        _lastInsertPostition = lastIns;
        return true;
    }
}
