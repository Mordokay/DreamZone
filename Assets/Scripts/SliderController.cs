using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    public Slider healthSlider;
    public Slider woodSlider;
    public Slider dreamSparkSlider;
    public Slider constructionPasteSlider;

    public Text healthSliderText;
    public Text woodSliderText;
    public Text dreamSparkSliderText;
    public Text constructionPasteSliderText;

    GameObject player;
    PlayerStats stats;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
    }
	
	public void UpdateBars()
    {
        healthSliderText.text = "Health " + stats.health + "/" + stats.maxHealth;
        woodSliderText.text = "Wood " + stats.wood + "/" + stats.maxWood;
        dreamSparkSliderText.text = "Spark " + stats.dreamSpark + "/" + stats.maxDreamSpark;
        constructionPasteSliderText.text = "Paste " + stats.constructionPaste + "/" + stats.maxConstructionPaste;

        healthSlider.value = (float) stats.health / (float)stats.maxHealth;
        woodSlider.value = (float)stats.wood / (float)stats.maxWood;
        dreamSparkSlider.value = (float)stats.dreamSpark / (float)stats.maxDreamSpark;
        constructionPasteSlider.value = (float)stats.constructionPaste / (float)stats.maxConstructionPaste;
    }
}
