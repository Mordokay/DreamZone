using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    public GameObject bulletWave;
    public Transform bulletSpawn;
    public float bulletSpeed;

    public LayerMask raycastLayers;

    GameObject player;
    GameObject gm;
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager");
    }

	void Update () {
        if (Input.GetButtonDown("Fire1") && !gm.GetComponent<UIManager>().pausedGame)
        {
            this.GetComponent<Animator>().SetTrigger("Shoot");

            player.GetComponent<PlayerSoundManager>().PlayShootSound();

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, raycastLayers))
            {
                //Debug.Log(hit.transform.gameObject.name);

                Vector3 bulletDirection = hit.point - bulletSpawn.position;

                GameObject myBullet = Instantiate(bulletWave) as GameObject;
                myBullet.transform.position = bulletSpawn.position;
                myBullet.transform.rotation = player.transform.rotation;
                myBullet.transform.Rotate(-Vector3.up * 90);
                myBullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * bulletSpeed);
            }
            else
            {
                GameObject myBullet = Instantiate(bulletWave) as GameObject;
                myBullet.transform.position = bulletSpawn.position;
                myBullet.transform.rotation = player.transform.rotation;
                myBullet.transform.Rotate(-Vector3.up * 90);
                myBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed);
                Destroy(myBullet, 4.0f);
            }
            player.GetComponent<PlayerStats>().dreamSpark -= 5;
        }
    }
}
