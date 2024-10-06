
using System.Collections;

namespace Labyrinth.Composition.Interfaces;

//
// tuple<int, IMemento<T> state>
// 
//


public interface IEditorHistory<T> : ICloneable, IEnumerator<IMemento<T>>
 where T : IReadOnlyCollection<IMemento<T>>
{
    public bool Undo();
    public bool Restore(IMemento<T> State);
    public void Add(IMemento<T> State);
    public bool Remove(IMemento<T> State);
    public bool RemoveLast();
}


public class EditorHistory<T> : IEditorHistory<T> where T : IReadOnlyCollection<IMemento<T>>
{

    //
    // Current Branch
    //
    private List<IMemento<T>> Writers { get; set; }

    private bool disposedValue;
    private int _index;
    private int I
    {
        get
        {
            return (_index > 0) ? _index : 0;
        }
        set
        {
            _index = (value > 0) ? value : 0;
        }
    }

    public IMemento<T> Current => Writers[I];

    object IEnumerator.Current => throw new NotImplementedException();

    public void Add(IMemento<T> State)
    {
        Writers.Add(State);
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }

    public bool MoveNext()
    {
        if(I < Writers.Count)
        {
            I++;
            return true;
        }
        return false;
    }
    //
    // will remove State if its last, use to check if current object you're on is the last.
    // could remove entire points from the state => usecase would be to remove parts of path you don't like
    // and then generate path from there.
    //
    public bool Remove(IMemento<T> State)
    {
        int a = 0;
        if((a = Writers.FindIndex(x => x == State)) == Writers.Count - 1)
        {
            Writers.RemoveAt(a);
            return true;
        }
        return false;
    }
    //
    //will in fact remove the last object.
    //
    public bool RemoveLast()
    {
        Writers.RemoveAt(Writers.Count);
        return true;
    }
    //
    // Will Restore the Caret/Writer to a previous point in time or in relation to it's current position, forward or back
    // returns false if not possible.
    //
    public bool Restore(IMemento<T> State)
    {
        var a = Writers.FindIndex(x => x == State);
        return false;

    }
    //
    // Push Caret back in the current timeline one action.
    //
    public bool Undo()
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~EditorHistory()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    void IEnumerator.Reset()
    {
        _index = -1;
    }
}
