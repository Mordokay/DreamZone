using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterController : MonoBehaviour {

    public GameObject paste;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void RemoveCrater()
    {
        GameObject myPaste = Instantiate(paste) as GameObject;
        myPaste.transform.position = this.transform.position;

        this.transform.parent.gameObject.GetComponent<GridBox>().isWall = false;
        this.transform.parent.gameObject.GetComponent<GridBox>().hasBuilding = false;
        Destroy(this.gameObject);


        player.GetComponent<PlayerStats>().score += 5;
    }
}