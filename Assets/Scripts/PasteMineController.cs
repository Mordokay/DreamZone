using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasteMineController : MonoBehaviour {

    public bool harvested;
    public float timeToRestore;
    public float timeHarvested;

    public GameObject particleEffect;
    public GameObject constructionPaste;

    void Start()
    {
        timeHarvested = 0.0f;
    }

    public void HarvestMine()
    {
        particleEffect.SetActive(false);
        harvested = true;

        GameObject myPaste = Instantiate(constructionPaste);
        myPaste.transform.position = this.transform.position;
    }

    public void RestoreMine()
    {
        particleEffect.SetActive(true); 
        harvested = false;
    }

    void Update()
    {
        if (harvested)
        {
            timeHarvested += Time.deltaTime;
            if (timeHarvested > timeToRestore)
            {
                RestoreMine();
                timeHarvested = 0.0f;
            }
        }
    }
}
