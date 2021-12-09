using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject polePrefab;
    GameObject lastPole;
    public GameObject roadPrefab;
    bool creating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartConstruction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopConstruction();

        }
        else
        {
            if (creating)
            {
                UpdateConstruction();
            }

        }
    }

    private void StartConstruction()
    {
        creating = true;
        Vector3 startPosition = getWorldPoint();
        startPosition = floorPosition(startPosition);
        GameObject startPole = Instantiate(polePrefab, new Vector3(startPosition.x, 0, startPosition.z), Quaternion.identity);
        startPole.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        lastPole = startPole;


    }

    private void StopConstruction()
    {
        creating = false;
    }

    private void UpdateConstruction()
    {
        Vector3 current = getWorldPoint();
        current = floorPosition(current);
        current = new Vector3(current.x, current.y + 0.3f, current.z);
        if (!current.Equals(lastPole.transform.position))
        {
            CreateRoadSegment(current);
        }
    }

    private void CreateRoadSegment(Vector3 current)
    {
        GameObject newPole = Instantiate(polePrefab, new Vector3(current.x, 0, current.z), Quaternion.identity);
        Vector3 middle = Vector3.Lerp(newPole.transform.position, lastPole.transform.position, 0.5f);
        GameObject newWall = Instantiate(roadPrefab, new Vector3(middle.x, 0, middle.z), Quaternion.identity);
        newWall.transform.LookAt(lastPole.transform);
        lastPole = newPole;
    }

    Vector3 getWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    Vector3 floorPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x);
        snapped.y = Mathf.Floor(original.y);
        snapped.z = Mathf.Floor(original.z);
        return snapped;
    }
}
