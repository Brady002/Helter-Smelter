using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : Receptical
{
    public override bool CheckRecepticalType(GameObject thing)
    {
        if (recepticalInventory == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
