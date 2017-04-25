using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objToSpwn;
    public float timeBetweenSpawns;

    public float currentTime;

    public Transform spawnPos;
    public GameManager gm;

	void Start () {
        currentTime = 0.0f;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	void Update () {
        timeBetweenSpawns -= Time.deltaTime * 0.005f;

        if(timeBetweenSpawns < 4.0f)
        {
            timeBetweenSpawns = 4.0f;
        }
        currentTime += Time.deltaTime;
        if(currentTime > timeBetweenSpawns)
        {
            currentTime = 0.0f;
            //Spawn the shadow
            GameObject myObjToSpwn = Instantiate(objToSpwn) as GameObject;
            myObjToSpwn.transform.position = spawnPos.position;
            gm.enemies.Add(myObjToSpwn);
        }
	}
}
