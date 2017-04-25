using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    GameManager gm;
    GameObject player;
    
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player.GetComponent<PlayerStats>().health -= 30;
            player.GetComponent<PlayerSoundManager>().PlayOutchSound();
            Destroy(this.gameObject);
        }
    }
}
