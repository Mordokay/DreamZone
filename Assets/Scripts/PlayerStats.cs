using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    //Goes from 0 to 1000
    public int wood;
    //Goes from 0 to 500
    public int constructionPaste;
    //Goes from 0 to 100
    public int dreamJuice;

    public int health;

	void Start () {
        wood = 200;
        dreamJuice = 50;
        constructionPaste = 200;
    }
	
    public void ClampItems()
    {
        Mathf.Clamp(this.wood, 0, 1000);
        Mathf.Clamp(this.constructionPaste, 0, 500);
        Mathf.Clamp(this.dreamJuice, 0, 100);
    }
}
