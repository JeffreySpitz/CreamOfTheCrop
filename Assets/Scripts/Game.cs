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
                PlantAnaysisUpdate();
                PlantSeedsUpdate();
                PlantUpdate();
                SproutUpdate();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        UserInput();
        UpdateZoom();
        UpdateCamPosition();
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
                    grid[x, y].crop = Cell.Crop.Flower;
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

    private void UpdateCamPosition()
    {
        if (camera_follow_position.x > SCREEN_WIDTH - 12f)
            camera_follow_position.x = SCREEN_WIDTH - 12f;
        if (camera_follow_position.x < -12f)
            camera_follow_position.x = -12f;
        if (camera_follow_position.z > SCREEN_HEIGHT - 22f)
            camera_follow_position.z = SCREEN_HEIGHT - 22f;
        if (camera_follow_position.z < -22f)
            camera_follow_position.z = -22f;
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

    void PlaceCells()
    {
        for (int y=0; y<SCREEN_HEIGHT; y++)
        {
            for (int x=0; x<SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(cell_prefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                grid[x, y] = cell;
                grid[x, y].crop = RandomCrop();
                grid[x, y].SetAlive(false);
                Debug.Log(grid[x, y].crop);
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

    Cell.Crop RandomCrop()
    {
        int rand = Random.Range(0, (int)Cell.Crop.NumOfTypes);
        return (Cell.Crop) rand;
    }

    void PlantAnaysisUpdate()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
                grid[x, y].AnalyzeGrid(grid, x, y);
    }

    void PlantUpdate()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
                grid[x, y].UpdateStatus();
    }

    void PlantSeedsUpdate()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
                grid[x, y].PlantSeeds(grid, x, y);
    }

    void SproutUpdate()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
            for (int x = 0; x < SCREEN_WIDTH; x++)
                grid[x, y].Sprout(grid, x, y);
    }

}
