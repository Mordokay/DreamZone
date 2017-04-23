using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public MyPathNode[,] grid;
    public GameObject gridBox;

    public GameObject map;
    public int gridWidth;
    public int gridHeight;
    public float gridSize;
    public static string distanceType;

    public static int distance = 2;

    void Start()
    {
        bakeObstacles();
    }

    public void createGrid()
    {
        removeGrid();
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                GameObject nobj = (GameObject)GameObject.Instantiate(gridBox);
                nobj.transform.position = new Vector3(gridBox.transform.position.x + (gridSize * j), 0.0f, gridBox.transform.position.y + (gridSize * i));
                nobj.name = j + "," + i;

                nobj.gameObject.transform.parent = map.transform;
                nobj.SetActive(true);
            }
        }
    }

    public void removeGrid()
    {
        while (map.transform.childCount > 0)
        {
            //Debug.Log(map.transform.childCount);
            Transform child = map.transform.GetChild(0);
            child.parent = null;
            DestroyImmediate(child.gameObject);
        }
    }

    public void bakeObstacles()
    {
        grid = new MyPathNode[gridWidth, gridHeight];

        foreach (Transform gridBox in map.transform)
        {
            string[] entries = gridBox.gameObject.name.Split(',');
            grid[int.Parse(entries[0]), int.Parse(entries[1])] = new MyPathNode()
            {
                IsWall = gridBox.gameObject.GetComponent<GridBox>().isWall,
                X = int.Parse(entries[0]),
                Y = int.Parse(entries[1]),
            };
        }
    }

    public void addWall(int x, int y)
    {
        grid[x, y].IsWall = true;
    }

    public void removeWall(int x, int y)
    {
        grid[x, y].IsWall = false;
    }
}
