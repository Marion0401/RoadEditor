using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject polePrefab;
    GameObject lastPole;
    public GameObject roadPrefab;
    bool creating;
    Vector3 currentPosition;
    GridEditor grid;

    

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.DeleteRoad(GetPositionGrid());
        }

        if (Input.GetMouseButton(1))
        {
            grid.AddRoad(GetPositionGrid());
        }
    }



    private Vector3 GetPositionGrid()
    {
        Vector3 position = getWorldPoint();
        position = floorPosition(position);
        return position;
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
