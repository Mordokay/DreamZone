using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientOneController : MonoBehaviour {

    GameObject gm;

    public float health;
    public float shieldHealth;

    public GameObject shield;
    public bool hasShieldActive;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager");
	}

    public void EnableShield()
    {
        shield.SetActive(true);
        hasShieldActive = true;
        shieldHealth = 500;
    }

    public void DisableShield()
    {
        shield.SetActive(false);
        hasShieldActive = false;
    }

    public void LoseLife(int life)
    {
        if (!hasShieldActive)
        {
            this.health -= life;
            if (health < 0.0f)
            {
                gm.GetComponent<UIManager>().GameOver();
            }
        }
        else
        {
            this.shieldHealth -= life;
            if (shieldHealth < 0.0f)
            {
                DisableShield();
            }
        }
    }
}
