using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Cell
{
    private int numSand = 0;
    private Vector2[] sandPositions = new Vector2[25];

    public int updatesPerSandSpread = 3;
    private int updateCounter = 0;

    public override void CollectActionInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        updateCounter++;
        if (updateCounter >= updatesPerSandSpread)
        {
            numSand = 0;
            for (int dy = -2; dy < 3; dy++)
            {
                for (int dx = -2; dx < 3; dx++)
                {
                    int nx = x + dx;
                    int ny = y + dy;
                    if (dx == 0 && dy == 0)
                        continue;
                    if (nx < 0 || nx >= grid.GetLength(0))
                        continue;
                    if (ny < 0 || ny >= grid.GetLength(1))
                        continue;
                    sandPositions[numSand] = new Vector2(nx, ny);
                    numSand++;
                }
            }
        }
    }

    public override void PerformAction(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        if (updateCounter >= updatesPerSandSpread)
        {
            for (int i = 0; i < numSand; i++)
            {
                Vector2 p = sandPositions[i];
                cellManager.SetCell((int)p.x, (int)p.y, CellType.Sand);
            }
            updateCounter = 0;
        }
    }
}
