using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem2D : MonoBehaviour
{
    //The camera gameObject
    public Camera cam;

    //How fast the camera moves
    public float moveSpeed = 6f;

    //How fast the camera zooms
    public float zoomSpeed = 6f;

    //Constraints on how far or close the camera can zoom
    public float maxCamSize;
    public float minCamSize;

    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;

    private void Update()
    {
        HandleCameraMovementDragPan();

        //Makes sure the current size is within the max and min sizes
        if (cam.orthographicSize < minCamSize)
        {
            cam.orthographicSize = minCamSize;
        }
        else if (cam.orthographicSize > maxCamSize)
        {
            cam.orthographicSize = maxCamSize;
        }
    }

    //Manages the camera drag input
    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //Drags from using right-click button
        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 1f;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.y = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = -transform.up * inputDir.y + -transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        //Manages the zoom system
        if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && (cam.orthographicSize >= minCamSize && cam.orthographicSize <= maxCamSize))
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
    }
}
