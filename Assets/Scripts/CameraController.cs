using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera target_camera;
    private Func<float> GetCameraZoomFunc;
    private Func<Vector3> GetCameraFollowPositionFunc;

    public float camera_zoom_speed = 5f;
    public float camera_move_speed = 2f;

    public void Setup(Func<float> GetCameraZoomFunc, Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    // Start is called before the first frame update
    void Start()
    {
        target_camera = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePosition();
        HandleZoom();
    }

    private void HandlePosition()
    {
        Vector3 camera_follow_position = GetCameraFollowPositionFunc();



        camera_follow_position.y = transform.position.y;

        Vector3 camera_move_direction = (camera_follow_position - transform.position).normalized;
        float distance = Vector3.Distance(camera_follow_position, transform.position);
        
        if (distance > 0)
        {
            Vector3 new_camera_position = transform.position + camera_move_direction * distance * camera_move_speed * Time.deltaTime;
            float distance_after_moving = Vector3.Distance(new_camera_position, camera_follow_position);

            if (distance_after_moving > distance)
            {
                // Overshot the target
                new_camera_position = camera_follow_position;
            }

            transform.position = new_camera_position;
        }
    }

    private void HandleZoom()
    {
        float camera_zoom = GetCameraZoomFunc();

        float zoom_diff = camera_zoom - target_camera.orthographicSize;

        target_camera.orthographicSize += zoom_diff * camera_zoom_speed * Time.deltaTime;

        if (zoom_diff > 0)
        {
            if (target_camera.orthographicSize > camera_zoom) // we are overshooting the target zoom
            {
                target_camera.orthographicSize = camera_zoom;
            }
        } else
        {
            if (target_camera.orthographicSize < camera_zoom) // we are undershooting target zoom
            {
                target_camera.orthographicSize = camera_zoom;
            }
        }
    }
}
