using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformController : MonoBehaviour {

    public bool activated;
    public GameObject wall;

    public Color original;

    public void RevertToOriginal()
    {
        this.GetComponent<Renderer>().material.color = original;
    }
}
