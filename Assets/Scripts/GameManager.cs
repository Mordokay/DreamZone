﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MyPathNode[,] grid;
    public GameObject gridBox;

    public GameObject map;
    public int gridWidth;
    public int gridHeight;
    public float gridSize;
    public static string distanceType;

    public static int distance = 2;
    public GameObject precious;
    public GameObject spawner;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bakeObstacles();
        InitializePlayer();
        AddSpawners();
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

    //removes gameobjects in an area
    void CleanNear(int x, int y)
    {
        for (int i = x - 2; i <= x + 2; i++)
        {
            for (int j = y - 2; j <= y + 2; j++)
            {
                foreach (Transform t in map.transform)
                {
                    if (t.name.Equals(i + "," + j))
                    {
                        GameObject wallAux = t.gameObject.GetComponent<BasicPlatformController>().wall;

                        //removes all objects in this area
                        var children = new List<GameObject>();
                        foreach (Transform child in t.gameObject.transform)
                        {
                            if (!child.gameObject.tag.Equals("MapWall"))
                            {
                                children.Add(child.gameObject);
                            }
                        }

                        children.ForEach(child => Destroy(child));

                        //restores the wall
                        break;
                    }
                }
            }
        }
    }

    void AddSpawners()
    {
        GameObject mySpawner = Instantiate(spawner) as GameObject;
        int X = Random.Range(0, gridWidth);
        int Y = Random.Range(0, 5);
        mySpawner.transform.position = new Vector3(X, 0.5f, Y);
        CleanNear(X, Y);

        mySpawner = Instantiate(spawner) as GameObject;
        X = Random.Range(gridWidth - 5, gridWidth);
        Y = Random.Range(0, gridHeight);
        mySpawner.transform.position = new Vector3(X, 0.5f, Y);
        CleanNear(X, Y);

        mySpawner = Instantiate(spawner) as GameObject;
        X = Random.Range(0, gridWidth);
        Y = Random.Range(gridHeight - 5, gridHeight);
        mySpawner.transform.position = new Vector3(X, 0.5f, Y);
        CleanNear(X, Y);

        mySpawner = Instantiate(spawner) as GameObject;
        X = Random.Range(0, 5);
        Y = Random.Range(0, gridHeight);
        mySpawner.transform.position = new Vector3(X, 0.5f, Y);
        CleanNear(X, Y);
    }

    void InitializePlayer()
    {
        new System.Random(map.GetComponent<MapTerrainGenerator>().seed);
        int X = gridWidth / 2;
        int Y = gridHeight / 2;
        player.transform.localPosition = new Vector3(X, 2.0f, Y);
        GameObject myPrecious = Instantiate(precious) as GameObject;
        myPrecious.transform.localPosition = new Vector3(X + 1, 1.0f, Y + 1);
        for (int i = X - 2; i <= X + 2; i++)
        {
            for (int j = Y - 2; j <= Y + 2; j++)
            {
                foreach (Transform t in map.transform)
                {
                    if (t.name.Equals(i + "," + j))
                    {
                        
                            t.gameObject.GetComponent<BasicPlatformController>().wall.SetActive(false);
                            t.gameObject.GetComponent<BasicPlatformController>().activated = true;
                            t.gameObject.GetComponent<MeshRenderer>().enabled = true;

                        //removes all objects in this area
                        var children = new List<GameObject>();
                        foreach (Transform child in t.gameObject.transform) children.Add(child.gameObject);
                        children.ForEach(child => Destroy(child));
                        break;
                    }
                }
            }
        }
    }
}
