﻿using System.Runtime.CompilerServices;

namespace Labyrinth.Composition.Interfaces;

//well we got a little sidetracked, point here being it will be ez
//logic sailing from here

//Icomparabale could be moved out of here into the layer above to 
// ensure comparison homogenuity.....
public interface ILine : IComparable<ILine>
{
    int LineStart { get; set; }
    int LineEnd { get; set; }
    int Len { get; }
    char LastInsert { get; set; }
    int LastInsertPosition { get; set; }

    static bool operator <(ILine b, ILine a) => (b.LineStart < a.LineStart);
    static bool operator >(ILine b, ILine a) => (b.LineStart > a.LineStart);
}













