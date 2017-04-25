using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float rotationSpeed;
    public float moveToPlayerSpeed;
    GameObject player;

    public enum itemType
    {
        wood,
        dreamJuice,
        constructionPaste,
        healthGlobe

    };

    public itemType type;
    public int quantity;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        this.transform.Rotate(Vector3.one * rotationSpeed);

        if(Vector3.Distance(this.transform.position, player.transform.position) < 3.0f)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveToPlayerSpeed);

    }
    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            switch (type)
            {
                case itemType.wood:
                    collision.gameObject.GetComponent<PlayerStats>().wood += quantity;
                    break;
                case itemType.dreamJuice:
                    collision.gameObject.GetComponent<PlayerStats>().dreamSpark += quantity;
                    break;
                case itemType.constructionPaste:
                    collision.gameObject.GetComponent<PlayerStats>().constructionPaste += quantity;
                    break;
                case itemType.healthGlobe:
                    collision.gameObject.GetComponent<PlayerStats>().health += quantity;
                    break;
            }
            collision.gameObject.GetComponent<PlayerSoundManager>().PlayPickSound();
            collision.gameObject.GetComponent<PlayerStats>().ClampItems();
            Destroy(this.gameObject);
        }
    }
}
