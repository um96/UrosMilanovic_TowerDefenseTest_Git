using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchasable : MonoBehaviour
{
    // Start is called before the first frame update
    public int cost;
    /// <summary>
    /// Dictates if this option should be disabled (such as, no longer possible to ugprade speed, or max amt. of towers reached)
    /// </summary>
    public bool can_purchase;
    GameMasterControls GMC;
    EnablePurchasableOptions EPO;
    public FireBullet fire_control;
    public string default_price_string;

    private void Start()
    {
        GMC =  GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterControls>();
        EPO = transform.parent.GetComponent<EnablePurchasableOptions>();

    }



    public void PurchaseUpgradeSpeed()
    {
        GMC.ChangePoints(-cost);
        fire_control.UpgradeBulletCD(.1f);
        if (fire_control.GetBulletCD() < .2)
        {
            can_purchase = false;
        }
        cost = cost + 30;
        EPO.UpdatePurchasables();
        
    }
    //Enter placement mode. Turn off menu and turn on option to click tiles to place tower there
    //Selected tiles are marked as occupied, then game resumes where it was in the buy menu
    public void PurchasePlaceTower()
    {
        GMC.is_placing_tower = true;
        GMC.ChangePoints(-cost);

        //Turn off buy menu while placing tower!
        foreach (Transform menu_item in transform.parent)
        {
            if(menu_item.GetComponent<Text>() != null)
            {
                menu_item.gameObject.SetActive(true);
            }
            else
            {
                menu_item.gameObject.SetActive(false);

            }
        }
        cost = cost + 30;
        EPO.UpdatePurchasables();

    }


}
