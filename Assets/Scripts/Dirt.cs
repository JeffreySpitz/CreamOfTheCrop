using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dirt : Cell
{
    public override void StatusUpdate(Cell[,] grid, int x, int y, CellManager cellManager)
    {
        int max_value = num_seeds.Max();

        if (max_value > 0)
        {
            int max_index = num_seeds.ToList().IndexOf(max_value);
            CellType cellType = (CellType)max_index;

            if (cellType == CellType.Corn)
            {
                int neighbors = Corn.CountNeighbors(grid, x, y);
                if (neighbors == 0)
                    cellManager.SetCell(x, y, CellType.Corn);
            }
        }

        num_seeds = new int[(int)CellType.NumberOfTypes]; // Clear seeds
    }
}
