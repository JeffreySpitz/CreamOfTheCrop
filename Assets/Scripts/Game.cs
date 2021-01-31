using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    private int SCREEN_WIDTH;
    private int SCREEN_HEIGHT;
    private float zoom = 3.4f;
    private float ret_zoom = 20f;  // zoom returned to the camera
    private Vector3 camera_follow_position;
    private Vector3 forward, right;
    private float edgeSize = 10f;

    public float speed = 0.01f;
    public CameraController camera_controller;
    public Camera camera_object;
    public float camera_keyboard_move_amount = 10f;
    public float camera_mouse_move_amount = 5f;
    public CellManager cellManager;
    public CellType cellType;
    public MusicManager2 musicManager;

    // Start is called before the first frame update
    void Start()
    {
        SCREEN_WIDTH = cellManager.LEVEL_WIDTH;
        SCREEN_HEIGHT = cellManager.LEVEL_HEIGHT;
        camera_controller.Setup(() => ret_zoom, () => camera_follow_position);

        forward =  camera_object.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        musicManager.PlayPlaylist();
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
        UpdateZoom();
        UpdateCamPosition();
    }

    void UserInput()
    {
        if (Input.GetMouseButtonDown(0)) // left mouse button
        {
            if ((Input.mousePosition.y / Screen.height) > 0.25f)
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
                        cellManager.SetCell(x, y, cellType);
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            // Pause / Resume simulation
            cellManager.gameIsPaused = !cellManager.gameIsPaused;
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

}
