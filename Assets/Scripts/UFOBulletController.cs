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
            gm.GetComponent<UIManager>().GameOver();
            Destroy(this.gameObject);
        }
    }
}
