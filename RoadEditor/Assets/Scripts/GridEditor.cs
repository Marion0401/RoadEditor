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


    // Start is called before the first frame update
    void Start()
    {
        Roads = new GameObject[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Roads[i, j] = Instantiate<GameObject>(Forward[0], new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
