using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBulletController : MonoBehaviour {

    GameObject gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().LoseLife(5);
            Destroy(this.gameObject);
        }
    }
}
