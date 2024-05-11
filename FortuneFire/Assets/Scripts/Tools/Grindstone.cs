using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
public class Grindstone : Receptical
{
    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.swordState == SwordState.FINISHED)
        {
            HowLong(attributes);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void HowLong(ItemAttributes attributes)
    {
        switch (attributes.metal)
        {
            case 0: StartCoroutine(Grind(3, attributes));
                canGrab = false;
                break;
            case 1: StartCoroutine(Grind(7, attributes));
                canGrab = false;
                break;
            case 2: StartCoroutine(Grind(12, attributes));
                canGrab = false;
                break;
            default: StartCoroutine(Grind(0, attributes));
                canGrab = true;
                break;
        }
    }

    private IEnumerator Grind(int time, ItemAttributes attributes)
    {
        bar.GetComponent<ProgressBar>().FillProgressBar(time);
        yield return new WaitForSeconds(time);
        attributes.sharpenNum++;
        yield return canGrab = true;
    }
}