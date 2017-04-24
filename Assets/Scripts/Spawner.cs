using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objToSpwn;
    public float timeBetweenSpawns;

    public float currentTime;

    public Transform spawnPos;

	void Start () {
        currentTime = 0.0f;	
	}
	
	void Update () {
        currentTime += Time.deltaTime;
        if(currentTime > timeBetweenSpawns)
        {
            currentTime = 0.0f;
            //Spawn the shadow
            GameObject myObjToSpwn = Instantiate(objToSpwn) as GameObject;
            myObjToSpwn.transform.position = spawnPos.position;
        }
	}
}
