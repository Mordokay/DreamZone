using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {


    GameManager gm;

    GameObject nearestEnemy;

    public GameObject bullet;
    public Transform bulletSpawnPos;

    public float minDistanceToShoot;

    public float bulletVelocity;

    float timeSinceLastShoot;
    public float timeBetweenShoots;

    GameObject player;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeSinceLastShoot = 0.0f;
    }
	
    void Shoot()
    {
        float minDistanceFound = 9999;

        foreach (GameObject myEnemy in gm.enemies)
        {
            if (myEnemy != null)
            {
                if (Vector3.Distance(this.transform.position, myEnemy.transform.position) < minDistanceToShoot &&
                        Vector3.Distance(this.transform.position, myEnemy.transform.position) < minDistanceFound)
                {
                    minDistanceFound = Vector3.Distance(this.transform.position, myEnemy.transform.position);
                    nearestEnemy = myEnemy;
                }
            }
        }
        if (minDistanceFound != 9999)
        {
            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPos.position, nearestEnemy.transform.position - bulletSpawnPos.position, out hit) && hit.transform.tag == nearestEnemy.tag)
            {
                Vector3 bulletDirection = nearestEnemy.transform.position - bulletSpawnPos.position;

                GameObject myBullet = Instantiate(bullet) as GameObject;
                myBullet.transform.position = bulletSpawnPos.position;

                myBullet.GetComponent<Rigidbody>().AddForce(bulletDirection.normalized * bulletVelocity);
                timeSinceLastShoot = 0.0f;
            }
        }
        else
        {
            nearestEnemy = null;
        }
    }

	void Update () {
        if (nearestEnemy)
        {
            this.transform.LookAt(nearestEnemy.transform);
            this.transform.Rotate(-Vector3.up * 90);
        }
        
        else if (this.transform.eulerAngles.x != 0 && this.transform.eulerAngles.z != 0)
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.identity, Time.deltaTime * 15.0f);
            if (Quaternion.Angle(this.transform.rotation, Quaternion.identity) < 1.0f)
            {
                this.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * 30);
        }

        timeSinceLastShoot += Time.deltaTime;
        if(timeSinceLastShoot > timeBetweenShoots)
        {
            Shoot();
        }
	}
}
