using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapTerrainGenerator : MonoBehaviour {

    public int sizeX;
    public int sizeY;

    public Noise.NormalizeMode normalizedMode;
    [Tooltip("Generates a map based on a number")]
    public int seed = 1234;
    public Vector2 offset;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity;
    public int[ , ] mapHeight;


    public bool hideObjects;

    public Vector2 mapCenter = Vector2.zero;

    float[,] noiseMap;

    [System.Serializable]
    public class NoiseElement{

        [SerializeField]
        public GameObject myObject;
        [SerializeField]
        public float minTheshhold;
        [SerializeField]
        public float maxTheshold;
        [SerializeField]
        public float upShift;
        [SerializeField]
        public bool isWalkable;
    }

    [SerializeField]
    public List<NoiseElement> myItems;

    void Reset()
    {
        sizeX = 40;
        sizeY = 40;
        seed = 12345;
        noiseScale = 5;
        octaves = 2;
        persistence = 0.56f;
        lacunarity = 0;
        mapCenter = Vector2.zero;
        offset = Vector2.zero;
        normalizedMode = Noise.NormalizeMode.Global;
    }

    void Start()
    {
        seed = PlayerPrefs.GetInt("seed");
        CreateHeight();
    }

    void OnInspectorGUI()
    {
        Debug.Log("Changed something!!!");
        //CreateHeight();
    }

    void CreateHeight()
    {
        mapHeight = new int[sizeX, sizeY];

        noiseMap = Noise.GeneratedNoiseMap(sizeX, sizeY, seed, noiseScale, octaves, persistence, lacunarity, mapCenter + offset, normalizedMode);

        foreach (Transform gridBox in this.transform)
        {
            string[] entries = gridBox.gameObject.name.Split(',');
            float greyColor = noiseMap[int.Parse(entries[0]), int.Parse(entries[1])];

            //put shades of brown instead
            gridBox.gameObject.GetComponent<Renderer>().material.color = new Color((40 + 100 * greyColor) / 255.0f, (30 + 70 * greyColor) / 255.0f, (10 + 10 * greyColor) / 255.0f);
            foreach (NoiseElement el in myItems)
            {
                //Instanciate  object
                if(greyColor > el.minTheshhold && greyColor < el.maxTheshold)
                {
                    GameObject myObj = Instantiate(el.myObject) as GameObject;
                    myObj.transform.position = gridBox.transform.position;
                    myObj.transform.Translate(Vector3.up * el.upShift);
                    gridBox.GetComponent<GridBox>().AddObjToGrid(myObj);

                    if (!el.isWalkable)
                    {
                        gridBox.GetComponent<GridBox>().isWall = true;
                    }
                }
            }
        }
    }
}
