using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientOneController : MonoBehaviour {

    GameObject gm;

    public float health;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager");
	}

    public void LoseLife(int life)
    {
        this.health -= life;
        if (health < 0.0f)
        {
            gm.GetComponent<UIManager>().GameOver();
        }
    }
}
