using System.Collections;
using System.Linq.Expressions;

namespace Labyrinth.Composition.Interfaces;

public interface ILineBank : IEnumerable<ILine>, IList<ILine>
{

}

public class LineBank : ILineBank
{
    private int It { get; set; }
    
    private ILine[] Lines { get; set; }

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public ILine this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //doesnt this mean it has to be transient? wont 
    public LineBank(IParameterizedFactory<IEnumerable<ILine>> line,
        IEnumerator<ILine> enumerator)
    {

    }
    public LineBank(IParameterizedFactory<IEnumerable<ILine>> Existing)
    {
        
    }
    //this creates issues asf
    //doesnt this mean it has to be transient?  
    public LineBank(IParameterizedFactory<ILine> line)
    {
        
    }
    public LineBank()
    {

    }
    public IEnumerator<ILine> GetEnumerator()
    {
        return (IEnumerator<ILine>)Lines.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return Lines.GetEnumerator();
    }

    public int IndexOf(ILine item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, ILine item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public void Add(ILine item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(ILine item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(ILine[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(ILine item)
    {
        return false;
    }
}
