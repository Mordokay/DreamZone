﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

    public GameObject brokenTree;

    public bool broken;
    public float timeToRestore;
    public float timeBroken;

    public GameObject logs;

    void Start () {
        timeBroken = 0.0f;
    }
	
	public void BreakTree()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        brokenTree.SetActive(true);
        this.GetComponent<MeshRenderer>().enabled = false;
        broken = true;
        GameObject myLogs = Instantiate(logs);
        myLogs.transform.position = this.transform.position;
    }

    public void RestoreTree()
    {
        this.GetComponent<BoxCollider>().enabled = true;
        brokenTree.SetActive(false);
        this.GetComponent<MeshRenderer>().enabled = true;
        broken = false;
    }

    void Update () {
        if (broken)
        {
            timeBroken += Time.deltaTime;
            if(timeBroken > timeToRestore)
            {
                RestoreTree();
                timeBroken = 0.0f;
            }
        }
	}
}