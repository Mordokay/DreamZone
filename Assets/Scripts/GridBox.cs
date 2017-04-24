using UnityEngine;
using System.Collections;

public class GridBox : MonoBehaviour {

    public bool isWall;
    public bool hasBuilding;

    public void AddObjToGrid(GameObject myObject)
    {
        myObject.transform.parent = this.transform;
    }
}
