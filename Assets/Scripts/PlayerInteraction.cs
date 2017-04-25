using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPos;
    public float bulletSpeed;

    GameObject lastObjectHit;

    public GameObject turretGreen;
    public GameObject turretNormal;

    public GameObject trapGreen;
    public GameObject trapNormal;

    public GameObject temporaryPlacementObject;

    public enum Tool
    {
        None,
        Turret,
        Trap,
        AncientShield,
    };

    public Tool currentTool;

    public List<GameObject> PlacementButonsUI;

    public LayerMask raycastLayers;
    public GameManager gm;


    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //ActivateUI(0);
        lastObjectHit = new GameObject();
    }

    void ActivateUI(int buttonId)
    {
        for (int i = 0; i < PlacementButonsUI.Count; i++)
        {
            PlacementButonsUI[i].GetComponent<Image>().color = Color.white;
        }

        if (PlacementButonsUI[buttonId].GetComponent<Image>().color.Equals(Color.green))
        {
            return;
        }

        switch (buttonId)
        {    
            case 0:
                if (!currentTool.Equals(Tool.Turret))
                {
                    PlacementButonsUI[0].GetComponent<Image>().color = Color.green;
                    currentTool = Tool.Turret;

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 3.0f, raycastLayers))
                    {
                        if (hit.transform.gameObject.tag.Equals("Platform"))
                        {
                            GameObject myTurretGreen = Instantiate(turretGreen) as GameObject;
                            myTurretGreen.transform.position = hit.transform.gameObject.transform.position - Vector3.right * 2;
                            temporaryPlacementObject = myTurretGreen;
                        }
                    }
                }
                else
                {
                    Destroy(temporaryPlacementObject);
                    currentTool = Tool.None;
                }
                break;
            case 1:
                if (!currentTool.Equals(Tool.Trap))
                {
                    PlacementButonsUI[1].GetComponent<Image>().color = Color.green;
                    currentTool = Tool.Trap;

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 3.0f, raycastLayers))
                    {
                        if (hit.transform.gameObject.tag.Equals("Platform"))
                        {
                            GameObject myTrapGreen = Instantiate(trapGreen) as GameObject;
                            myTrapGreen.transform.position = hit.transform.gameObject.transform.position - Vector3.right * 2;
                            temporaryPlacementObject = myTrapGreen;
                        }
                    }
                }
                else
                {
                    Destroy(temporaryPlacementObject);
                    currentTool = Tool.None;
                }
                break;
            case 2:
                if (!currentTool.Equals(Tool.AncientShield))
                {
                    PlacementButonsUI[2].GetComponent<Image>().color = Color.green;
                    currentTool = Tool.AncientShield;
                }
                else
                {
                    Destroy(temporaryPlacementObject);
                    currentTool = Tool.None;
                }
                break;
        }
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Placing Turret");
            Destroy(temporaryPlacementObject);
            ActivateUI(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Placing Trap");
            Destroy(temporaryPlacementObject);
            ActivateUI(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Placing Ancient Shield");
            Destroy(temporaryPlacementObject);
            ActivateUI(2);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 3.0f, raycastLayers))
        {   
            if (hit.transform.gameObject.tag.Equals("Platform"))
            {
                switch (currentTool)
                {
                    case Tool.Turret:
                        if (!lastObjectHit.gameObject.Equals(hit.transform.gameObject) && !hit.transform.gameObject.GetComponent<GridBox>().hasBuilding
                            && !hit.transform.gameObject.GetComponent<GridBox>().hasTrap)
                        {
                            if (temporaryPlacementObject != null)
                            {
                                Destroy(temporaryPlacementObject);
                            }

                            GameObject myTurretGreen = Instantiate(turretGreen) as GameObject;
                            myTurretGreen.transform.position = hit.transform.gameObject.transform.position;
                            temporaryPlacementObject = myTurretGreen;
                        }
                        break;
                    case Tool.Trap:
                        if (!lastObjectHit.gameObject.Equals(hit.transform.gameObject) && !hit.transform.gameObject.GetComponent<GridBox>().hasBuilding 
                            && !hit.transform.gameObject.GetComponent<GridBox>().hasTrap)
                        {
                            if (temporaryPlacementObject != null)
                            {
                                Destroy(temporaryPlacementObject);
                            }

                            GameObject myTrapGreen = Instantiate(trapGreen) as GameObject;
                            myTrapGreen.transform.position = hit.transform.gameObject.transform.position;
                            temporaryPlacementObject = myTrapGreen;
                        }
                        break;
                }

                Transform objectHit = hit.transform;
                lastObjectHit = objectHit.gameObject;

                if (Input.GetButtonDown("Fire2"))
                {
                    switch (currentTool)
                    {
                        case Tool.Turret:
                            Debug.Log("Place Turret!!!");
                            if (temporaryPlacementObject && this.GetComponent<PlayerStats>().CanPlaceTurret() && !lastObjectHit.GetComponent<GridBox>().hasBuilding
                            && !hit.transform.gameObject.GetComponent<GridBox>().hasTrap)
                            {
                                lastObjectHit.GetComponent<GridBox>().hasBuilding = true;

                                this.GetComponent<PlayerStats>().PlaceTurret();
                                this.GetComponent<PlayerSoundManager>().PlayUnlockTileSound();

                                GameObject myTurret = Instantiate(turretNormal) as GameObject;
                                myTurret.transform.position = temporaryPlacementObject.transform.position;

                                Destroy(temporaryPlacementObject);
                            }

                            break;
                        case Tool.Trap:
                            Debug.Log("Place Trap!!!");
                            if (temporaryPlacementObject && this.GetComponent<PlayerStats>().CanPlaceTrap() && !lastObjectHit.GetComponent<GridBox>().hasBuilding
                            && !hit.transform.gameObject.GetComponent<GridBox>().hasTrap)
                            {
                                lastObjectHit.GetComponent<GridBox>().hasTrap = true;

                                this.GetComponent<PlayerStats>().PlaceTrap();
                                this.GetComponent<PlayerSoundManager>().PlayUnlockTileSound();

                                GameObject myTrap = Instantiate(trapNormal) as GameObject;
                                myTrap.transform.position = temporaryPlacementObject.transform.position;

                                Destroy(temporaryPlacementObject);

                                gm.allTraps.Add(myTrap);
                            }
                            break;
                    }
                }
            }
            else
            {
                Destroy(temporaryPlacementObject);
                if (hit.transform.gameObject.tag.Equals("Tree") && !hit.transform.GetComponent<TreeController>().broken)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.gameObject.GetComponent<TreeController>().BreakTree();
                    }
                }
                else if (hit.transform.gameObject.tag.Equals("PasteMine") && !hit.transform.gameObject.GetComponent<PasteMineController>().harvested)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.gameObject.GetComponent<PasteMineController>().HarvestMine();
                    }
                }
                else if (hit.transform.gameObject.tag.Equals("HealthTree") && !hit.transform.gameObject.GetComponent<HealthTreeController>().harvested)
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.gameObject.GetComponent<HealthTreeController>().HarvestTree();
                    }
                }
                else if (hit.transform.gameObject.tag.Equals("Crater"))
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.gameObject.GetComponent<CraterController>().RemoveCrater();
                    }
                }
                else if (hit.transform.gameObject.tag.Equals("Precious") && currentTool.Equals(Tool.AncientShield) &&
                    this.GetComponent<PlayerStats>().CanPlaceShield())
                {
                    if (Input.GetButtonDown("Fire2"))
                    {
                        hit.transform.gameObject.GetComponent<AncientOneController>().EnableShield();
                        this.GetComponent<PlayerStats>().PlaceShield();
                        Debug.Log("Put a shield on the ancient one!!!");
                    }
                }
            }
        }
        else
        {
            Destroy(temporaryPlacementObject);
        }
    }
}
