using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float zoom = 5;
    private float zoomCamera;
    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x+0.1f, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-0.1f);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            zoomCamera -= zoom;
            zoomCamera = Mathf.Clamp(zoomCamera, 40, 100);
            GetComponent<Camera>().fieldOfView = zoomCamera;

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            zoomCamera += zoom;
            zoomCamera = Mathf.Clamp(zoomCamera, 40, 100);
            GetComponent<Camera>().fieldOfView = zoomCamera;

        }
    }
}
