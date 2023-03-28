using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnablePurchasableOptions : MonoBehaviour
{
    // Start is called before the first frame update
    GameMasterControls GMC;
    public List<GameObject> purchasable_list;
    void Awake()
    {
        GMC = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterControls>();
    }



    //Updates button Data
    //Specifically, the price shown on the tooltips and whether the buttons are interactable or not.
    //Buying an item increases its future price, so this is called on opening the Buy menu or making a purchase
    public void UpdatePurchasables()
    {
        if(GMC == null)
        {
            Awake();
        }
        for(int i = 0; i < purchasable_list.Count; i ++)
        {
            if(purchasable_list[i].GetComponent<Purchasable>() != null)
            {
                //Set current price in title
                string temp = purchasable_list[i].GetComponent<Purchasable>().default_price_string;
                purchasable_list[i].transform.GetChild(0).GetComponent<Text>().text = string.Format(temp, purchasable_list[i].GetComponent<Purchasable>().cost.ToString());


                if(purchasable_list[i].GetComponent<Purchasable>().cost <= GMC.points && purchasable_list[i].GetComponent<Purchasable>().can_purchase)
                {
                    purchasable_list[i].GetComponent<Button>().interactable = true;
                }
                else if (!purchasable_list[i].GetComponent<Purchasable>().can_purchase)
                {
                    //cannot use (max upgrade reached)
                    purchasable_list[i].transform.GetChild(0).GetComponent<Text>().text = "OUT OF STOCK";
                    purchasable_list[i].GetComponent<Button>().interactable = false;

                }
                else
                {
                    //Cannot use - can't afford cost
                    purchasable_list[i].GetComponent<Button>().interactable = false;

                }
            }
        }
    }



}
