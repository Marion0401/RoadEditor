using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEditor : MonoBehaviour
{
    public int height = 100;
    public int width;
    public GameObject[,] Roads;
    public List<GameObject> Forward = new List<GameObject>();
    public List<GameObject> Intersect = new List<GameObject>();
    public List<GameObject> Turn = new List<GameObject>();
    public List<GameObject> T_Road = new List<GameObject>();
    public GameObject End_Road;
    public List<Vector3> Directions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Roads = new GameObject[height, width];
        Directions.Add(Vector3.forward);
        Directions.Add(Vector3.right);
        Directions.Add(Vector3.back);
        Directions.Add(Vector3.left);

        //for (int i = 0; i < height; i++)
        //{
        //    for (int j = 0; j < width; j++)
        //    {
        //        Roads[i, j] = Instantiate<GameObject>(Forward[0], new Vector3(i, 0, j), Quaternion.identity);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddRoad(Vector3 position)
    {
        if (IsInGrid((int)position.x,(int)position.z))
        {
            if (Roads[(int)position.x, (int)position.z] == null)
            {
                Roads[(int)position.x, (int)position.z] = Instantiate<GameObject>(Forward[0], new Vector3(position.x,0,position.z), Quaternion.identity);
                CheckNeighbors(position);
            }

        }

    }
    public void DeleteRoad(Vector3 pos)
    {
        if (IsInGrid((int)pos.x, (int)pos.z))
        {
            if (Roads[(int)pos.x, (int)pos.z] != null)
            {
                Destroy(Roads[(int)pos.x, (int)pos.z]);
                Roads[(int)pos.x, (int)pos.z] = null;
            }
        }
    }



    public void CheckNeighbors(Vector3 pos)
    {
        bool[] neighbors = new bool[4];
        int NeighborCount = 0;
        int compteur = 0;
        foreach (var dir in Directions)
        {
            Vector3 future = pos + dir;
            if (IsInGrid(future))
            {

            }
            compteur++;
        }
    }

    public bool IsInGrid(int x, int y)
    {
        return (x >= 0 && x < height && y >= 0 && y < width);
    }
    public bool IsInGrid(Vector3 target)
    {
        return (target.x >= 0 && target.x < height && target.z >= 0 && target.z < width);
    }

}
