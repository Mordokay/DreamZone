using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRockRotation : MonoBehaviour {


	void Start () {
        this.transform.Rotate(Vector3.up * Random.Range(0, 360));
	}
	
}
