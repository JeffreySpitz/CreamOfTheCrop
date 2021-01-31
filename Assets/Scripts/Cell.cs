using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = true;
    public int[] num_seeds = new int[(int)CellType.NumberOfTypes];
    public CellType cellType;

    public virtual void CollectActionInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {

    }

    public virtual void PerformAction(Cell[,] grid, int x, int y, CellManager cellManager)
    {

    }

    public virtual void CollectStatusInfo(Cell[,] grid, int x, int y, CellManager cellManager)
    {

    }

    public virtual void StatusUpdate(Cell[,] grid, int x, int y, CellManager cellManager)
    {

    }
}
