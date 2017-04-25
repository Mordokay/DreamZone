using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("CollidedWith " + collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Tree")){
            collision.transform.parent.gameObject.GetComponent<GridBox>().isWall = false;

            Destroy(collision.transform.gameObject);
        }
        if (collision.gameObject.tag.Equals("HealthTree"))
        {
            collision.transform.parent.gameObject.GetComponent<GridBox>().isWall = false;

            Destroy(collision.transform.gameObject);
        }
        if (collision.gameObject.tag.Equals("UFO"))
        {
            collision.transform.gameObject.GetComponent<UFOController>().DropDreamJuice();

            Destroy(collision.transform.gameObject);
        }
        if (collision.gameObject.tag.Equals("Platform"))
        {
            collision.transform.gameObject.GetComponent<GridBox>().MakeCrater();
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.transform.gameObject.GetComponent<PlayerStats>().health -= 50;
        }
        if (collision.gameObject.tag.Equals("Shadow"))
        {
            collision.transform.gameObject.GetComponent<ShadowEnemy>().DropDreamJuice();
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("PasteMine"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
