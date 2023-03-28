using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public bool is_occupied = false;
    GameMasterControls GMC;
    ObjectPools pools;
    GameObject GM;
    MainMenuControls MMC;
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameMaster");
        GMC = GM.GetComponent<GameMasterControls>();
        pools = GM.GetComponent<ObjectPools>();

        MMC = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainMenuControls>();

    }

    private void OnMouseDown()    
    {
        if(Input.GetMouseButtonDown(0) && GMC.is_placing_tower && !is_occupied)
        {
            is_occupied = true;
            GetComponent<Renderer>().enabled = false;
            CreateTower();
            GMC.is_placing_tower = false;
            MMC.ReturnToBuyMenuAfterTowerPlace();
        }
    }


    void CreateTower()
    {
       /* GameObject Tower = */ pools.UsePool(ObjectPools.InstantiableObjects.Tower, transform.position, Quaternion.identity);
    }
}
