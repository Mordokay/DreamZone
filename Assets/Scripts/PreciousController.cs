using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciousController : MonoBehaviour {

    GameObject gm;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager");
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Ancient collision!!! " + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Shadow"))
        {
            //game ends
            gm.GetComponent<UIManager>().GameOver();
        }
    }
}
