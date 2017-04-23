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

    void Start()
    {
        goingUp = true;
        currentAlpha = minAlpha;
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
