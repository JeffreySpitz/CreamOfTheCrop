using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Crop { Conway, Flower, NumOfTypes };

    public bool isAlive = false;
    public Crop crop = Crop.Flower;
    public int num_neighbors = 0;

    public int[] num_seeds = new int[(int)Crop.NumOfTypes];

    public Renderer conway_renderer;
    public Renderer flower_renderer;
    public Renderer dirt_renderer;
    public void SetAppearance()
    {
        conway_renderer.enabled = false;
        flower_renderer.enabled = false;
        dirt_renderer.enabled = false;

        if (isAlive)
        {
            if (crop == Crop.Conway)
                conway_renderer.enabled = true;
            else if (crop == Crop.Flower)
                flower_renderer.enabled = true;
        }
        else
            dirt_renderer.enabled = true;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        SetAppearance();
    }

    public void AnalyzeGrid(Cell[,] grid, int x, int y)
    {
        if (crop == Crop.Conway)
        {
            num_neighbors = 0;
            for (int dy = -1; dy < 2; dy++)
            {
                for (int dx = -1; dx < 2; dx++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    int nx = x + dx;
                    int ny = y + dy;
                    if (nx < 0 || nx >= grid.GetLength(0))
                        continue;
                    if (ny < 0 || ny >= grid.GetLength(1))
                        continue;

                    if (grid[nx, ny].isAlive && grid[nx, ny].crop == Crop.Conway)
                        num_neighbors++;
                }
            }
        }
        if (crop == Crop.Flower)
        {
            num_neighbors = 0;
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
                    num_neighbors++;
            }
        }
    }

    public void UpdateStatus()
    {
        if (crop == Crop.Conway)
            if (num_neighbors <= 1 || num_neighbors >= 4)
                SetAlive(false);
        if (crop == Crop.Flower)
            if (num_neighbors > 0)
                SetAlive(false);
    }

    public void PlantSeeds(Cell[,] grid, int x, int y)
    {
        if (isAlive)
        {
            if (crop == Crop.Conway)
            {
                for (int dy = -1; dy < 2; dy++)
                {
                    for (int dx = -1; dx < 2; dx++)
                    {
                        if (dx == 0 && dy == 0)
                        {
                            continue;
                        }

                        int nx = x + dx;
                        int ny = y + dy;
                        if (nx < 0 || nx >= grid.GetLength(0))
                            continue;
                        if (ny < 0 || ny >= grid.GetLength(1))
                            continue;

                        grid[nx, ny].num_seeds[(int)crop]++;
                    }
                }
            }
            else if (crop == Crop.Flower)
            {
                int dx = Random.Range(-2, 3);
                int dy = Random.Range(-2, 3);
                int nx = x + dx;
                int ny = y + dy;
                if (nx < 0 || nx >= grid.GetLength(0))
                    return;
                if (ny < 0 || ny >= grid.GetLength(1))
                    return;

                grid[nx, ny].num_seeds[(int)crop]++;
            }
        }
    }

    public void Sprout(Cell[,] grid, int x, int y)
    {
        if (!isAlive)
        {
            int max_value = num_seeds.Max();
            int max_index = num_seeds.ToList().IndexOf(max_value);
            crop = (Crop)max_index;

            if (crop == Crop.Conway)
                if (max_value == 3)
                    SetAlive(true);

            if (crop == Crop.Flower)
            {
                num_neighbors = 0;
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
                        num_neighbors++;
                }
                if (num_neighbors == 0)
                    SetAlive(true);
            }
        }
        num_seeds = new int[(int)Crop.NumOfTypes]; // reset seed counter
    }
}
