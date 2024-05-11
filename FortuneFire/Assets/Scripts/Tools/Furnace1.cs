using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Furnace1 : Receptical
{

    [SerializeField] private GameObject bucket;
    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.swordState == SwordState.ORE)
        {
            HowLong(attributes, thing);
            return true;
        } else
        {
            return false;
        }
    }
    private void HowLong(ItemAttributes attributes, GameObject thing)
    {;
        switch (attributes.metal)
        {
            case 0:
                StartCoroutine(Smelt(5, attributes, thing));
                canGrab = false;
                break;
            case 1:
                StartCoroutine(Smelt(10, attributes, thing));
                canGrab = false;
                break;
            case 2:
                StartCoroutine(Smelt(15, attributes, thing));
                canGrab = false;
                break;
            default:
                StartCoroutine(Smelt(0, attributes, thing));
                break;
        }
    }

    private IEnumerator Smelt(int smeltTime, ItemAttributes attributes, GameObject thing)
    {
        //GameObject bar = GameObject.Find("BarBG");
        bar.GetComponentInChildren<ProgressBar>().FillProgressBar(smeltTime);
        yield return new WaitForSeconds(smeltTime);
        Debug.Log(attributes.metal);
        GameObject bucketGO = Instantiate(bucket, attachPoint.transform.position, Quaternion.identity);
        Destroy(thing);
        recepticalInventory = bucketGO;
        bucketGO.GetComponent<ItemAttributes>().metal = attributes.metal;
        attributes.swordState = SwordState.MOLTEN;

        yield return canGrab = true;
        yield return attributes;
    }

    
}