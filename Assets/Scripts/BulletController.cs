using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float percentageIncrease;

    GameManager gm;
    GameObject player;

	void Start () {
        Destroy(this.gameObject, 5.0f);
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	


	void Update () {
        this.transform.localScale = this.transform.localScale +  
            new Vector3(Time.deltaTime * percentageIncrease, Time.deltaTime * percentageIncrease, Time.deltaTime * percentageIncrease) ;
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Shadow"))
        {
            Debug.Log("hit A Shadow!!!");
            collision.gameObject.GetComponent<ShadowEnemy>().DropDreamJuice();
            Destroy(this.gameObject);
            gm.CleanEnemiesList();

            player.GetComponent<PlayerStats>().score += 10;
            
        }
        else if(collision.gameObject.tag.Equals("UFO"))
        {
            Debug.Log("Hit a UFO!!!");
            collision.gameObject.GetComponent<UFOController>().DropDreamJuice();
            Destroy(this.gameObject);

            player.GetComponent<PlayerStats>().score += 20;
        }
        else if (collision.gameObject.tag.Equals("UFOBullet"))
        {
            Debug.Log("Hit a UFO Bullet!!!");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            player.GetComponent<PlayerStats>().score += 10;
        }
        else
        {
            Debug.Log("hit something else");
            Destroy(this.gameObject);
        }
    }
}
