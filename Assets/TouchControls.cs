using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    Vector3 touchStart;
    public Transform target;
    public float distance = 20.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float zoomOutMin = 2;
    public float zoomOutMax = 20;

    public Vector3 delta = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    float x = 0.0f;
    float y = 0.0f;
    GameObject[] array;

    void Update()
    {
        //Record first touch position
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        //Record second touch position
        if(Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
        }
        //Move camera
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 position = Camera.main.transform.position;
            position.x += direction.x * 0.01f;
            position.z += direction.y * 0.01f;
            Camera.main.transform.position = position;
        }
        // zoom
        if(Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Vector2 touchZeroPrevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
            Vector2 touchOnePrevPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;
            float prevMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;
            float difference = currentMag - prevMag;
            Zoom(difference * 0.01f);
        }
    }
    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
