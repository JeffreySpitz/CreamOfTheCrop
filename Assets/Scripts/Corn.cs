using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Cell
{

    public override void PerformAction(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        // Plant seeds
        int dx = Random.Range(-2, 3);
        int dy = Random.Range(-2, 3);
        int nx = x + dx;
        int ny = y + dy;
        if (nx < 0 || nx >= grid.GetLength(0))
            return;
        if (ny < 0 || ny >= grid.GetLength(1))
            return;

        grid[nx, ny].num_seeds[(int)CellType.Corn]++;
    }
}
