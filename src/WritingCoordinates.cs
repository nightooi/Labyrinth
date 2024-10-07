
using Labyrinth.Composition.Interfaces;

namespace Labyrinth.Composition;
public class WritingCoordinates : ICoordinates
{
    int _x, _y;
    char _insertion;
    public int X => _x;
    public int Y =>_y;
    public int Insertion => _insertion;

    public WritingCoordinates(int x, int y, char insertion)
    {
        _x = x;
        _y = y;
        _insertion = insertion;
    }
}

