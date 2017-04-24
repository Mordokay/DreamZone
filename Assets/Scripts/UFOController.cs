using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {

    GameObject player;
    public float moveSpeed;

    public float intervalBeteeenShooting;
    public float timeSincelastShoot;

    public GameObject UFOBullet;
    public float bulletSpeed;

    public Transform bulletSpawnPoint;
    public GameObject dreamJuice;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        timeSincelastShoot = 0.0f;
    }

    public void DropDreamJuice()
    {
        GameObject myJuice = Instantiate(dreamJuice);
        myJuice.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

    void Update()
    {
        timeSincelastShoot += Time.deltaTime;

        if (Vector3.Distance(player.transform.position, this.transform.position) > 4.0f)
        {
            this.transform.position = Vector3.MoveTowards(
                this.transform.position, new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), Time.deltaTime * moveSpeed);
        }

        if (Vector3.Distance(player.transform.position, this.transform.position) < 6.0f)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if(timeSincelastShoot > intervalBeteeenShooting)
        {
            GameObject myBullet = Instantiate(UFOBullet) as GameObject;
            myBullet.transform.position = bulletSpawnPoint.position;
            myBullet.GetComponent<Rigidbody>().AddForce((player.transform.position - this.transform.position).normalized * bulletSpeed);
            timeSincelastShoot = 0.0f;

            Physics.IgnoreCollision(myBullet.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
