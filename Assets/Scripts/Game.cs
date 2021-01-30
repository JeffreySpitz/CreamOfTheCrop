using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT = 48;
    private float timer = 0f;
    private float zoom = 3.4f;
    private float ret_zoom = 20f;  // zoom returned to the camera
    private Vector3 camera_follow_position;
    private Vector3 forward, right;
    private float edgeSize = 10f;

    public int birth_requirement = 3; // Any dead cell with exactly 'birth_threshold' cells next to it becomes alive
    public int crowded_threshold = 4; // Any live cell with at least 'crowd_threshold' cells next to it dies
    public int lonliness_threshold = 1; // Any live cell with at most 'lonliness_threshold' cells next to it dies
    public Cell cell_prefab;
    public float speed = 0.01f;
    public CameraController camera_controller;
    public Camera camera_object;
    public float camera_keyboard_move_amount = 10f;
    public float camera_mouse_move_amount = 5f;

    public bool simulation_enabled = false;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];


    // Start is called before the first frame update
    void Start()
    {
        camera_controller.Setup(() => ret_zoom, () => camera_follow_position);
        PlaceCells();

        forward =  camera_object.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulation_enabled)
        {
            if (timer >= speed)
            {
                timer = 0f;
                CountNeighbors();
                PopulationControl();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        UserInput();
        UpdateZoom();
    }

    void UserInput()
    {
        if (Input.GetMouseButtonDown(0)) // left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mouse_point = hit.point;

                int x = Mathf.RoundToInt(mouse_point.x);
                int y = Mathf.RoundToInt(mouse_point.z);

                if (x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
                {
                    grid[x, y].SetAlive(!grid[x, y].isAlive);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            // Pause / Resume simulation
            simulation_enabled = !simulation_enabled;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
                ZoomIn();
            else
                ZoomOut();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y > Screen.height - edgeSize)
            camera_follow_position += forward * camera_keyboard_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < edgeSize)
            camera_follow_position -= right * camera_keyboard_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y < edgeSize)
            camera_follow_position -= forward * camera_keyboard_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > Screen.width - edgeSize)
            camera_follow_position += right * camera_keyboard_move_amount * (zoom + 1) * Time.deltaTime;

        if (Input.mousePosition.y > Screen.height - edgeSize)
            camera_follow_position += forward * camera_mouse_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.mousePosition.x < edgeSize)
            camera_follow_position -= right * camera_mouse_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.mousePosition.y < edgeSize)
            camera_follow_position -= forward * camera_mouse_move_amount * (zoom + 1) * Time.deltaTime;
        if (Input.mousePosition.x > Screen.width - edgeSize)
            camera_follow_position += right * camera_mouse_move_amount * (zoom + 1) * Time.deltaTime;


    }

    private void ZoomIn()
    {
        zoom -= .5f;
        if (zoom < 0f) zoom = 0f;
    }

    private void ZoomOut()
    {
        zoom += .5f;
        if (zoom > 3.5f) zoom = 3.5f;
    }

    private void UpdateZoom()
    {
        ret_zoom = Mathf.Exp(zoom);
    }

    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

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
                        if (nx < 0)
                        {
                            nx += SCREEN_WIDTH;
                        }
                        if (nx >= SCREEN_WIDTH)
                            nx %= SCREEN_WIDTH;


                        if (ny < 0)
                        {
                            ny += SCREEN_HEIGHT;
                        }

                        if (ny >= SCREEN_HEIGHT)
                            ny %= SCREEN_HEIGHT;

                        if (ny >= 0 && ny < SCREEN_HEIGHT)
                        {
                            if (nx >= 0 && nx < SCREEN_WIDTH)
                            {
                                if (grid[nx, ny].isAlive)
                                {
                                    numNeighbors++;
                                }
                            }

                        }
                    }
                }
                grid[x, y].numNeighbors = numNeighbors;
            }
        }
    }

    void PopulationControl()
    {
        for (int y = 0; y<SCREEN_HEIGHT; y++)
        {
            for (int x=0; x<SCREEN_WIDTH; x++)
            {
                if (grid[x,y].isAlive)
                {
                    if (grid[x,y].numNeighbors <= lonliness_threshold || grid[x, y].numNeighbors >= crowded_threshold)
                    {
                        grid[x,y].SetAlive(false);
                    }
                } else
                {
                    if (grid[x,y].numNeighbors == birth_requirement)
                    {
                        grid[x,y].SetAlive(true);
                    }
                }
            }
        }
    }

    void PlaceCells()
    {
        for (int y=0; y<SCREEN_HEIGHT; y++)
        {
            for (int x=0; x<SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(cell_prefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    bool RandomAliveCell()
    {
        int rand = Random.Range(0, 100);
        if (rand>75)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
