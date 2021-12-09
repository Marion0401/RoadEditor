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
        if (IsInGrid((int)position.x, (int)position.z))
        {
            if (Roads[(int)position.x, (int)position.z] == null)
            {
                GameObject test = Roads[(int)position.x, (int)position.z] = Instantiate<GameObject>(Forward[0], new Vector3(position.x, 0, position.z), Quaternion.identity);
                //test.transform.SetParent(transform, false);
                CheckNeighbors(position, false, Vector3.up);
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


    public void CheckNeighbors(Vector3 pos, bool dontCheck, Vector3 origin)
    {

        bool[] neighbors = new bool[4];
        int compteur = 0;
        int neighborCount = 0;
        foreach (var dir in Directions)
        {
            Vector3 future = pos + dir;

            if (IsInGrid(future))
            {
                if (Roads[(int)future.x, (int)future.z] != null)
                {

                    neighborCount++;
                    neighbors[compteur] = true;
                    if (!dontCheck && future != origin)
                    {
                        CheckNeighbors(future, true, pos);
                    }

                }

            }
            compteur++;
        }
        ChangeModel(pos, neighbors, neighborCount);
    }


    public void ChangeModel(Vector3 pos, bool[] neighbors, int count)
    {
        if (count == 4)
        {
            DeleteRoad(pos);
            Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Intersect[0], new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        }
        if (count == 3)
        {
            DeleteRoad(pos);
            Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(T_Road[0], new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        }
        if (count == 2)
        {
            DeleteRoad(pos);
            if (neighbors[0] && neighbors[2])
            {
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Forward[0], new Vector3(pos.x, 0, pos.z), Quaternion.identity);
                Roads[(int)pos.x, (int)pos.z].transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (neighbors[1] && neighbors[3])
            {
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Forward[0], new Vector3(pos.x, 0, pos.z), Quaternion.identity);
            }
            else
            {
                if (neighbors[0] && neighbors[1])
                    Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Turn[0], new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, 0, 0));
                else if (neighbors[1] && neighbors[2])
                    Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Turn[0], new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, 90, 0));
                else if (neighbors[2] && neighbors[3])
                    Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Turn[0], new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, 180, 0));
                else if (neighbors[3] && neighbors[0])
                    Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Turn[0], new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, -90, 0));
                //Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(Turn[0], new Vector3(pos.x, 0, pos.z), Quaternion.identity);
            }
        }
        if (count == 0)
        {
            DeleteRoad(pos);

            Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(End_Road, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
        }
        if (count == 1)
        {
            DeleteRoad(pos);
            if (neighbors[3])
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(End_Road, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
            else if (neighbors[2])
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(End_Road, new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, -90, 0));
            else if (neighbors[1])
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(End_Road, new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, 180, 0));
            else
                Roads[(int)pos.x, (int)pos.z] = Instantiate<GameObject>(End_Road, new Vector3(pos.x, 0, pos.z), Quaternion.Euler(0, 90, 0));

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
