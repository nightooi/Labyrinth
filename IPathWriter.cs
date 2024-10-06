using Labyrinth.Composition.Interfaces;

using System.Text;
//pathwriter maintains state for where the insertion takes place 
//pathwriter is commanded by direction to write in and how many signs.
public interface IPathWriter : ICloneable
{
    public StringBuilder InsertDown(int len, StringBuilder field);
    public StringBuilder InsertLeft(int len, StringBuilder field);
    public StringBuilder InsertRight(int len, StringBuilder field);
    public StringBuilder InsertUp(int len, StringBuilder field);
}

