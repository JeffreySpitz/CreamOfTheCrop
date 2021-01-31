using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Cell
{

    public int burnTime = 5;

    private int updateCounter = 0;
    private int numBurns = 0;
    private Vector2[] burnPositions = new Vector2[9];
    public override void CollectActionInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        numBurns = 0;
        for (int dy = -1; dy < 2; dy++)
        {
            for (int dx = -1; dx < 2; dx++)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx < 0 || nx >= grid.GetLength(0))
                    continue;
                if (ny < 0 || ny >= grid.GetLength(1))
                    continue;
                if (grid[nx, ny].isAlive)
                {
                    burnPositions[numBurns] = new Vector2(nx, ny);
                    numBurns++;
                }
            }
        }
    }

    public override void PerformAction(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        for (int i = 0; i < numBurns; i++)
        {
            Vector2 p = burnPositions[i];
            cellManager.SetCell((int)p.x, (int)p.y, CellType.Fire);
        }
    }

    public override void CollectStatusInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        updateCounter++;
    }

    public override void StatusUpdate(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        if (updateCounter>= burnTime)
        {
            cellManager.SetCell(x, y, CellType.Dirt);
        }
    }
}
