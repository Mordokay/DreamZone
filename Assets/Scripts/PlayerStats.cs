using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerStats : MonoBehaviour {

    public int wood;    
    public int constructionPaste;
    public int dreamSpark;
    public float health;

    public int score;
    public int highscore;

    public Text scoreText;
    public Text highscoreText;

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

    public bool takingDamage = false;
    bool damageUp = true;
    float maxRedDamage = 0.3f;
    float currentRedDamage = 0.0f;

    public Text woodTurret;
    public Text sparkTurret;
    public Text pasteTurret;

    public Text woodTrap;
    public Text sparkTrap;
    public Text pasteTrap;

    public Text woodShield;
    public Text sparkShield;
    public Text pasteShield;

    void Start () {

        score = 0;
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = "HIGHSCORE: " + highscore;

        gm = GameObject.FindGameObjectWithTag("GameManager");

        wood = 300;
        constructionPaste = 150;
        dreamSpark = 100;

        health = 100.0f;

        timeSinceLastRefill = 0.0f;

        UpdateMaterialCosts();
    }

    public void UpdateSliders()
    {
        sliderController.UpdateBars();
    }

    private void Update()
    {

        if (takingDamage)
        {
            if (damageUp)
            {
                currentRedDamage += Time.deltaTime;
                Camera.main.gameObject.GetComponent<EdgeDetection>().edgesOnly = currentRedDamage;

                if(currentRedDamage >= maxRedDamage)
                {
                    currentRedDamage = maxRedDamage;
                    damageUp = false;
                }
            }
            else
            {
                currentRedDamage -= Time.deltaTime;
                Camera.main.gameObject.GetComponent<EdgeDetection>().edgesOnly = currentRedDamage;
                if (currentRedDamage < 0)
                {
                    currentRedDamage = 0.0f;
                    takingDamage = false;
                    damageUp = true;
                }
            }
        }

        ClampItems();

        scoreText.text = "SCORE: " + score;
        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = "HIGHSCORE: " + highscore;
        }
        if (health < 100)
        {
            health += Time.deltaTime * healthIncreaseRatio;
        }

        if (health <= 0)
        {
            gm.GetComponent<UIManager>().GameOver();
        }

        timeSinceLastRefill += Time.deltaTime;

        if(timeSinceLastRefill > refilTime)
        {
            wood += 5;
            constructionPaste += 2;
            dreamSpark += 1;

            UpdateSliders();
            UpdateMaterialCosts();

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

    public void UpdateMaterialCosts()
    {
        if (constructionPaste < 50)
        {
            pasteTurret.color = Color.red;
        }
        else
        {
            pasteTurret.color = Color.white;
        }
        if (wood < 300) {
            woodTurret.color = Color.red;
        }
        else
        {
            woodTurret.color = Color.white;
        }
        if ( dreamSpark < 5)
        {
            sparkTurret.color = Color.red;
        }
        else
        {
            sparkTurret.color = Color.white;
        }

        //////////////////////////////////////////////////////////////
        if (constructionPaste < 10)
        {
            pasteTrap.color = Color.red;
        }
        else
        {
            pasteTrap.color = Color.white;
        }
        if (wood < 50)
        {
            woodTrap.color = Color.red;
        }
        else
        {
            woodTrap.color = Color.white;
        }
        if (dreamSpark < 5)
        {
            sparkTrap.color = Color.red;
        }
        else
        {
            sparkTrap.color = Color.white;
        }

        //////////////////////////////////////////////////////////////
        if (constructionPaste < 250)
        {
            pasteShield.color = Color.red;
        }
        else
        {
            pasteShield.color = Color.white;
        }
        if (wood < 500)
        {
            woodShield.color = Color.red;
        }
        else
        {
            woodShield.color = Color.white;
        }
        if (dreamSpark < 50)
        {
            sparkShield.color = Color.red;
        }
        else
        {
            sparkShield.color = Color.white;
        }
    }

    //Turret functions
    public bool CanPlaceTurret()
    {
        return (constructionPaste >= 50 && wood >= 200 && dreamSpark >= 20);
    }
    public void PlaceTurret()
    {
        constructionPaste -= 25;
        wood -= 200;
        dreamSpark -= 20;

        UpdateMaterialCosts();
    }

    //Trap functions
    public bool CanPlaceTrap()
    {
        return (constructionPaste >= 10 && wood >= 50 && dreamSpark >= 5);
    }
    public void PlaceTrap()
    {
        constructionPaste -= 10;
        wood -= 50;
        dreamSpark -= 5;

        UpdateMaterialCosts();
    }

    //Ancient Shield functions
    public bool CanPlaceShield()
    {
        return (constructionPaste >= 250 && wood >= 500 && dreamSpark > 50);
    }
    public void PlaceShield()
    {
        constructionPaste -= 250;
        wood -= 500;
        dreamSpark -= 50;

        UpdateMaterialCosts();
    }

    public void RemoveWood(int x)
    {
        this.wood -= x;
    }
}
