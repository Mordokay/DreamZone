using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public int xSize;
    public int ySize;

    List<Platform> myPlatforms;
    public GameObject platform;
    public float platformSize;

    public class Platform
    {
        int posX;
        int posY;
        GameObject myPlatform;

        public Platform(int x, int y, GameObject _platform)
        {
            this.posX = x;
            this.posY = y;
            this.myPlatform = _platform;
        }
    }

    void Start()
    {
        /*
        myPlatforms = new List<Platform>();

        for (int i = - (xSize / 2); i <= (xSize / 2); i++)
        {
            for (int j = - (ySize / 2); j <= (ySize / 2); j++)
            {
                GameObject myPlatform = Instantiate(platform) as GameObject;
                myPlatform.transform.position = new Vector3(i * platformSize, 0.0f, j * platformSize);
                myPlatforms.Add(new Platform(i, j, myPlatform));
            }
        }
        */
    }

    void Update () {
		
	}
}
