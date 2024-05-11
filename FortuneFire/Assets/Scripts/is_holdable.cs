using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class is_holdable : MonoBehaviour
{
    bool held = true;
    public void ChangeHoldStatus() {
        held = !held;
    }
}
