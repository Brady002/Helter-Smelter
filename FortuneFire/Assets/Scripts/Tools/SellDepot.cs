using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SellDepot : Receptical
{
    public TMP_Text moneyP1;       
    public TMP_Text moneyP2;       
    public int player; //0 is player 1, 1 is player 2 

    public GameObject p1Win;      
    public GameObject p2Win;      

    private int intMoneyCounter1;      
    private int intMoneyCounter2;      

    private int winAmount = 50;    

    private void Start() {
        p1Win.SetActive(false);
        p2Win.SetActive(false);
    }

    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.isSellable)
        {
            Sell(thing, attributes);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Sell(GameObject item, ItemAttributes components)
    {
        //Variables are metal type, sword stage, grind level, and if there is a handle
        int sellPrice = (components.MetalValue() + components.HandleValue() + components.SwordValue());
        sellPrice += item.gameObject.GetComponent<ItemAttributes>().sharpenNum * 2;

        if(player == 0) {
            intMoneyCounter1 += sellPrice;
            if(intMoneyCounter1 >= winAmount) {
                p1Win.SetActive(true);
            }
            moneyP1.text = intMoneyCounter1.ToString();
        } else {
            intMoneyCounter2 += sellPrice;
            if(intMoneyCounter2 >= winAmount) {
                p2Win.SetActive(true);
            }
            moneyP2.text = intMoneyCounter2.ToString();
        }
        Destroy(item.gameObject);

    }
}
