using UnityEngine;
using System.Collections;

public class GridBox : MonoBehaviour {

    public bool isWall;

    public GameObject objectHolder;

    public void AddObjToGrid(GameObject myObject)
    {
        myObject.transform.parent = objectHolder.transform;
    }

    public void HideObjects()
    {
        objectHolder.SetActive(false);
    }

    public void ShowObjects()
    {
        objectHolder.SetActive(true);
    }
}
