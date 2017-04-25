using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEnemy : MonoBehaviour {

    public float minAlpha;
    public float maxAlpha;

    public float currentAlpha;

    public bool goingUp;
    public float speed;

    public GameObject body;

    public GameObject dreamJuice;

    GameObject gm;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager");
        goingUp = true;
        currentAlpha = minAlpha;

        //TO make sure game doesnt become very slow .. destroys the shadow after 1 minute 30 seconds
        Destroy(this.gameObject, 90.0f);
    }

    public void DropDreamJuice()
    {
        GameObject myJuice = Instantiate(dreamJuice);
        myJuice.transform.position = this.transform.position;
        Destroy(this.gameObject);

        player.GetComponent<PlayerStats>().score += 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Precious"))
        {
            Debug.Log("Ancient collision!!! " );
            //game ends
            gm.GetComponent<UIManager>().GameOver();
        }
    }

    void Update () {

        if (goingUp)
        {
            currentAlpha += Time.deltaTime * speed;
            if(currentAlpha > maxAlpha)
            {
                goingUp = false;
                currentAlpha = maxAlpha;
            }
        }
        else
        {
            currentAlpha -= Time.deltaTime * speed;
            if (currentAlpha < minAlpha)
            {
                goingUp = true;
                currentAlpha = minAlpha;
            }
        }
        Color color = body.GetComponent<Renderer>().material.color;
        color.a = currentAlpha;
        body.GetComponent<Renderer>().material.color = color;
    }
}
