using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Cell
{
    private int numNeighbors = 0;

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

    public static int CountNeighbors(Cell[,] grid, int x, int y)
    {
        int neighbors = 0;
        Vector2[] root_positions = new Vector2[] {
                new Vector2( 1, 0),
                new Vector2(-1, 0),
                new Vector2( 0, 1),
                new Vector2( 0,-1)};
        foreach (Vector2 v in root_positions)
        {
            int nx = x + (int)v.x;
            int ny = y + (int)v.y;
            if (nx < 0 || nx >= grid.GetLength(0))
                continue;
            if (ny < 0 || ny >= grid.GetLength(1))
                continue;
            if (grid[nx, ny].isAlive)
                neighbors++;
        }
        return neighbors;
    }

    public override void CollectStatusInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        numNeighbors = CountNeighbors(grid, x, y);
    }

    public override void StatusUpdate(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        if (numNeighbors > 0)
            cellManager.SetCell(x, y, CellType.Dirt);
    }
}
