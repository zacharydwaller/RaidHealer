using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int Row { get; set; }
    public int Col { get; set; }

    public Coordinate(int row = 0, int col = 0)
    {
        Row = row;
        Col = col;
    }
}
