namespace Labyrinth.Composition.Interfaces;

public interface IEditableLine : ILine 
{ 
    bool AdjustStart(int len, char c, int lastIns);
    bool AdjustLen(int len, char c, int lastIns);
}
