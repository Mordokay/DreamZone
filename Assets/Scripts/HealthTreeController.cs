using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTreeController : MonoBehaviour {

    public GameObject berries;

    public bool harvested;
    public float timeToRestore;
    public float timeBroken;

    public GameObject healthGlobe;

    public Transform healthGlobeSpawnPos;

    void Start()
    {
        timeBroken = 0.0f;
    }

    public void HarvestTree()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        berries.SetActive(false);
        harvested = true;

        GameObject myLogs = Instantiate(healthGlobe);
        myLogs.transform.position = healthGlobeSpawnPos.position;
    }

    public void RestoreTree()
    {
        this.GetComponent<BoxCollider>().enabled = true;
        berries.SetActive(true);
        harvested = false;
    }

    void Update()
    {
        if (harvested)
        {
            timeBroken += Time.deltaTime;
            if (timeBroken > timeToRestore)
            {
                RestoreTree();
                timeBroken = 0.0f;
            }
        }
    }
}
