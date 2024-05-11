using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trash: Receptical
{

    public override bool CheckRecepticalType(GameObject thing)
    {
        attributes = thing.GetComponent<ItemAttributes>();
        if (recepticalInventory == null && attributes.isDestroyable)
        {
            DestroyItem(thing);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyItem(GameObject item)
    {
        Destroy(item);
    }

    //Destroy items sitting on top of the trash if they're destroyable
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent<ItemAttributes>(out ItemAttributes component))
        {
            if(component.isDestroyable) {
                Destroy(other.gameObject);
            }
        }
    }

}
