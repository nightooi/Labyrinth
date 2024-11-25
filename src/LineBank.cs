namespace Labyrinth.Composition.Interfaces;

public class LineBank : ILineBank<ILine>
{
    
    public LineBank(
        int endOfLineLen, 
        LinkedList<IEditableLine> bank,
        ILineFactoryFacade lineFactFacade)
    {
        this.EndOfLineLen = endOfLineLen;
        this._lineFactory = lineFactFacade;
        this._bank = bank;
    }
    private readonly int EndOfLineLen;
    private readonly ILineFactoryFacade _lineFactory;
    private LinkedList<IEditableLine> _bank;
    public IReadOnlyCollection<ILine> Bank => _bank;
    /// <summary>
    /// Append will expand the current line or decrease current line
    /// </summary>
    /// <param name="Line"></param>
    /// <param name="len"></param>
    /// <param name="c"></param>
    /// <param name="lastIns"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool AppendToLine(ILine Line, int len, char c, int lastIns)
    {
        //add new 
        throw new NotImplementedException();
    }
    /// <summary>
    /// A
    /// </summary>
    /// <param name="LineNumber"></param>
    /// <param name="len"></param>
    /// <param name="c"></param>
    /// <param name="lastIns"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool AdjustLine(int LineNumber, int len, char c, int lastIns)
    {
        var res = LineNumber - Bank.Count;
        if (LineNumber > 1) return false;
        else if (LineNumber == 1) { 
            IEditableLine lineA = this._lineFactory.Create();
            lineA.AdjustStart(_bank.Last!.Value.LineStart + EndOfLineLen,  );
            _bank.AddLast(lineA);
           return true;
        }
        LinkedListNode<IEditableLine> line;
        line = _bank.First!;
        for(int i = LineNumber; i > 0; i--)
        {
            if (line is null) return false;
            line = line.Next!;
        }
        line.ValueRef.AdjustLen(len, c, lastIns);
        if(LineNumber < Bank.Count) 
            while(line.Next is not null)
            {
                line.ValueRef.AdjustStart(len, c, lastIns);
            };
        return true;
    }

    public bool GetLine(int LineNumber, out ILine? item, int lastIns)
    {
        throw new NotImplementedException();
    }
    //first in the bank, not the latest written too
    public ref ILine GetFirst()
    {
        throw new NotImplementedException();
    }
    //returnes the last line in the bank, not the latest written 
    public ref ILine GetLast()
    {
        throw new NotImplementedException();
    }
    //returns the line which was edited most recently
    public ref ILine? GetLastest()
    {
        throw new NotImplementedException();
    }
    //removes the line
    //if the line is in the middle removes everything after
    public bool Remove(ILine item)
    {
        throw new NotImplementedException();
    }
    //removes the line
    //if the line is in the middle removes everything after
    public bool Remove(int item)
    {
        throw new NotImplementedException();
    }
}
