using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int wood;    
    public int constructionPaste;
    public int dreamSpark;
    public float health;

    public int maxWood = 1000;
    public int maxConstructionPaste = 500;
    public int maxDreamSpark = 200;
    public float maxHealth = 100;

    public float healthIncreaseRatio;

    GameObject gm;

    public SliderController sliderController;

    //Every X seconds player gains resources;
    public float refilTime;
    public float timeSinceLastRefill;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager");

        wood = 500;
        constructionPaste = 200;
        dreamSpark = 100;

        health = 100.0f;

        timeSinceLastRefill = 0.0f;
    }

    public void UpdateSliders()
    {
        sliderController.UpdateBars();
    }

    private void Update()
    {
        ClampItems();

        if (health < 100)
        {
            health += Time.deltaTime * healthIncreaseRatio;
        }

        timeSinceLastRefill += Time.deltaTime;

        if(timeSinceLastRefill > refilTime)
        {
            wood += 5;
            constructionPaste += 2;
            dreamSpark += 1;

            UpdateSliders();

            timeSinceLastRefill = 0.0f;
        }
    }

    public void LoseLife(int life)
    {
        this.health -= life;
        if(health < 0.0f)
        {
            gm.GetComponent<UIManager>().GameOver();
        }
    }

    public void ClampItems()
    {
        this.wood = Mathf.Clamp(this.wood, 0, maxWood);
        this.constructionPaste = Mathf.Clamp(this.constructionPaste, 0, maxConstructionPaste);
        this.dreamSpark = Mathf.Clamp(this.dreamSpark, 0, maxDreamSpark);
        this.health = Mathf.Clamp(this.health, 0, maxHealth);

        UpdateSliders();
    }

    //Turret functions
    public bool CanPlaceTurret()
    {
        return (constructionPaste >= 25 && wood >= 100 && dreamSpark >= 5);
    }
    public void PlaceTurret()
    {
        constructionPaste -= 25;
        wood -= 100;
        dreamSpark -= 5;
    }

    public void RemoveWood(int x)
    {
        this.wood -= x;
    }
}
