using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Receptical : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attachPoint;
    public GameObject recepticalInventory;
    public bool canGrab = true;
    public ItemAttributes attributes;

    public GameObject bar;

    public abstract bool CheckRecepticalType(GameObject item);
    public bool PutDown(GameObject item)
    {
        if(recepticalInventory == null)
        {
            if(CheckRecepticalType(item)) {
                recepticalInventory = item;
                attributes = item.GetComponent<ItemAttributes>();
                item.transform.position = attachPoint.position;
                return true;
            }
            return false;
        }
        return false;
    }
}
