using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour {

    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

    public float heightDrop;

    public GameObject meteor;

    public float intervalBetweenMeteors;
    float lastMeteorDrop;

    void Start () {
        lastMeteorDrop = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        lastMeteorDrop += Time.deltaTime;
        if(lastMeteorDrop > intervalBetweenMeteors)
        {
            lastMeteorDrop = 0.0f;
            GameObject myMeteor = Instantiate(meteor) as GameObject;
            myMeteor.transform.position = new Vector3(Random.Range(minX, maxX), heightDrop, Random.Range(minY, maxY));
        }
	}
}
