using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVertical : MonoBehaviour {

    public float speedRotation;
	
	void Update () {
        this.transform.Rotate(Vector3.back * Time.deltaTime * speedRotation);
	}
}
