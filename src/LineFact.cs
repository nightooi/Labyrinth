namespace Labyrinth.Composition.Interfaces;

public class LineFact : ILineFactoryFacade
{
    ISimpleFactory<ICompareLineByStart<ILine>> _line;

    public ILine Create()
    {
        return new Line(null, _line);
    }
    public LineFact(ISimpleFactory<ICompareLineByStart<ILine>> lineFact)
    {
        _line = lineFact;
    }
}
