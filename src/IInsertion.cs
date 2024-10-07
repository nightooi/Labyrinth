namespace Labyrinth.Composition.Interfaces;

public interface IInsertion
{
    ICoordinates Coordinates { get; init; } 
    char Insertion { get; init; }
    int Length { get; init; }
}
