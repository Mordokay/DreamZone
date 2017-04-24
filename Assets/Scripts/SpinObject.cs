using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {

    public float rotateSpeed;

	void Update () {
        this.transform.Rotate(Vector3.up * rotateSpeed);
	}
}
