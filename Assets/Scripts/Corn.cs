using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Cell
{
    public int updatesPerSeedSpread = 1;
    private int updateCounter = 0;

    public override void CollectActionInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        updateCounter++;
    }
    public override void PerformAction(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        if (updateCounter >= updatesPerSeedSpread)
        {
            updateCounter = 0;
            // Plant seeds
            int dx = Random.Range(-1, 2);
            int dy = Random.Range(-1, 2);
            int nx = x + dx;
            int ny = y + dy;
            if (nx < 0 || nx >= grid.GetLength(0))
                return;
            if (ny < 0 || ny >= grid.GetLength(1))
                return;

            grid[nx, ny].num_seeds[(int)CellType.Corn]++;
        }
    }
}
