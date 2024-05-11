using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : Receptical
{
    [SerializeField]
    private GameObject[] bladeArr;

    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.swordState == SwordState.INGOT)
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
            case 0:
                StartCoroutine(Forge(5, attributes, thing));
                canGrab = false;
                break;
            case 1:
                StartCoroutine(Forge(7, attributes, thing));
                canGrab = false;
                break;
            case 2:
                StartCoroutine(Forge(10, attributes, thing));
                canGrab = false;
                break;
            default:
                StartCoroutine(Forge(0, attributes, thing));
                canGrab = true;
                break;
        }
    }

    private IEnumerator Forge(int time, ItemAttributes attributes, GameObject thing)
    {
        bar.GetComponent<ProgressBar>().FillProgressBar(time);
        yield return new WaitForSeconds(time);

        GameObject blade = Instantiate(bladeArr[attributes.metal], attachPoint.transform.position, Quaternion.identity);
        Destroy(thing);
        recepticalInventory = blade;

        attributes.forged = true;
        attributes.swordState = SwordState.FINISHED;
        yield return canGrab = true;
    }

}
