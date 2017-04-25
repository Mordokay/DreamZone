using UnityEngine;
using System.Collections;

public class GridBox : MonoBehaviour {

    public bool isWall;
    public bool hasBuilding;
    public bool hasTrap;

    public GameObject Crater;

    public void AddObjToGrid(GameObject myObject)
    {
        myObject.transform.parent = this.transform;
    }

    public void MakeCrater()
    {
        //Debug.Log("Making crater!!!");
        GameObject myCrater = Instantiate(Crater) as GameObject;
        myCrater.transform.position = this.transform.position;
        myCrater.transform.parent = this.transform;
        this.isWall = true;
        this.hasBuilding = true;
    }
}
