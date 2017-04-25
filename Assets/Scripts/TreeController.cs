using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

    public GameObject brokenTree;

    public bool broken;
    public float timeToRestore;
    public float timeBroken;

    public GameObject logs;

    public Transform logSpawnPos;

    void Start () {
        timeBroken = 0.0f;
    }
	
	public void BreakTree()
    {
        this.GetComponents<BoxCollider>()[0].enabled = false;
        this.GetComponents<BoxCollider>()[1].enabled = false;
        brokenTree.SetActive(true);
        this.GetComponent<MeshRenderer>().enabled = false;
        broken = true;
        GameObject myLogs = Instantiate(logs);
        myLogs.transform.position = logSpawnPos.position;
    }

    public void RestoreTree()
    {
        this.GetComponents<BoxCollider>()[0].enabled = true;
        this.GetComponents<BoxCollider>()[1].enabled = true;
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
