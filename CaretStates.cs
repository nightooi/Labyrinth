using Labyrinth.Composition.Interfaces;

namespace Labyrinth.Composition;
public class CaretStates : IMemento<IInsertion>
{
    IInsertion _Items;
    public void Restore(IMemento<IInsertion> toState)
    {
        throw new NotImplementedException();
    }

    public IInsertion Save()
    {
        throw new NotImplementedException();
    }

}

