using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPos;
    public float bulletSpeed;

    GameObject lastObjectHit;


    public Material greenTransparent;
    public Material white;

    public LayerMask raycastLayers;

    void Update () {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
/*
        if (Physics.Raycast(ray, out hit, 3.0f, raycastLayers) && Input.GetButtonDown("Fire2"))
        {
            Debug.Log("hit object: " + hit.transform.name);
        }
  */      
        if (Physics.Raycast(ray, out hit, 3.0f, raycastLayers))
        {
            if (lastObjectHit && lastObjectHit.gameObject.tag.Equals("Platform"))
            {
                if (!lastObjectHit.GetComponent<BasicPlatformController>().activated)
                {
                    lastObjectHit.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    lastObjectHit.GetComponent<MeshRenderer>().enabled = true;
                    //lastObjectHit.GetComponent<Renderer>().material = Instantiate(white);
                    lastObjectHit.GetComponent<BasicPlatformController>().RevertToOriginal();
                }
            }

            Transform objectHit = hit.transform;
            lastObjectHit = objectHit.gameObject;
            
            if (lastObjectHit.gameObject.tag.Equals("Platform") && !lastObjectHit.GetComponent<BasicPlatformController>().activated)
            {
                lastObjectHit.GetComponent<MeshRenderer>().enabled = true;
                lastObjectHit.GetComponent<Renderer>().material = Instantiate(greenTransparent);
                if (Input.GetButtonDown("Fire2"))
                {
                    //Debug.Log("hit object: " + lastObjectHit.name);
                    if (lastObjectHit.tag.Equals("Platform") && !lastObjectHit.GetComponent<BasicPlatformController>().activated)
                    {
                        lastObjectHit.GetComponent<BasicPlatformController>().activated = true;
                        lastObjectHit.GetComponent<BasicPlatformController>().wall.SetActive(false);
                        //lastObjectHit.GetComponent<Renderer>().material = Instantiate(white);
                        lastObjectHit.GetComponent<BasicPlatformController>().RevertToOriginal();

                        //Shows hidden onjects
                        lastObjectHit.GetComponent<GridBox>().ShowObjects();
                    }
                }
            }
            else if (lastObjectHit.tag.Equals("Tree") && !lastObjectHit.GetComponent<TreeController>().broken)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    lastObjectHit.GetComponent<TreeController>().BreakTree();
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject myBullet = Instantiate(bullet) as GameObject;
            myBullet.transform.position = bulletPos.position;
            myBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed);
            Destroy(myBullet, 4.0f);
        }
    }
}
