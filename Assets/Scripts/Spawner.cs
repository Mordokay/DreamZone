using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject Shadow;
    public float timeBetweenSpawns;

    public float currentTime;

	void Start () {
        currentTime = 0.0f;	
	}
	
	void Update () {
        currentTime += Time.deltaTime;
        if(currentTime > timeBetweenSpawns)
        {
            currentTime = 0.0f;
            //Spawn the shadow
            GameObject myShadow = Instantiate(Shadow) as GameObject;
            myShadow.transform.position = this.transform.position;
        }
	}
}
