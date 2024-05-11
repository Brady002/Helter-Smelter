using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cast1 : Receptical
{
    [SerializeField]
    private GameObject[] ingotArr;

    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.swordState == SwordState.MOLTEN)
        {
            HowLong(attributes, thing);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void HowLong(ItemAttributes attributes, GameObject thing)
    {
        switch (attributes.metal)
        {
            case 0: StartCoroutine(Harden(5, attributes, thing));
                canGrab = false;
                break;
            case 1: StartCoroutine(Harden(8, attributes, thing));
                canGrab = false;
                break;
            case 2: StartCoroutine(Harden(12, attributes, thing));
                canGrab = false;
                break;
            default: StartCoroutine(Harden(0, attributes, thing));
                canGrab = true;
                break;
                
        }
    }

    private IEnumerator Harden(int time, ItemAttributes attributes, GameObject thing)
    {
        bar.GetComponent<ProgressBar>().FillProgressBar(time);
        yield return new WaitForSeconds(time);

        GameObject ingot = Instantiate(ingotArr[attributes.metal], attachPoint.transform.position, Quaternion.identity);
        Destroy(thing);
        recepticalInventory = ingot;

        attributes.swordState = SwordState.INGOT;
        attributes.isSellable = true;
        yield return canGrab = true;
    }
}
