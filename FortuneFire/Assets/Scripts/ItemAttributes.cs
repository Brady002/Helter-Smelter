using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordState { NONE, ORE, MOLTEN, INGOT, FINISHED, HANDLE }
public enum MetalType { NONE, COPPER, IRON, GOLD }
public enum HandleType { NONE, WOOD, METAL, ORNATE }
public class ItemAttributes : MonoBehaviour
{
    [Header("Sword Components")]
    public SwordState swordState;
    public MetalType metalType;
    public HandleType handleType;

    [SerializeField]
    public int metal;
    public bool isSellable;
    public bool isDestroyable;
    public bool inInventory;

    public bool forged;
    public int sharpenNum;

    public int MetalValue()
    {
        int metalValue = 0;
        switch (metalType)
        {
            case MetalType.COPPER: metalValue = 3; break;
            case MetalType.IRON: metalValue = 5; break;
            case MetalType.GOLD: metalValue = 10; break;
            default: metalValue = 0; break;
        }
        return metalValue;
    }
    public int HandleValue()
    {
        int handleValue = 0;
        switch (handleType)
        {
            case HandleType.WOOD: handleValue = 1; break;
            case HandleType.METAL: handleValue = 3; break;
            case HandleType.ORNATE: handleValue = 5; break;
            default: handleValue = 0; break;
        }
        return handleValue;
    }
    public int SwordValue()
    {
        int swordValue = 0;
        switch (swordState)
        {
            case SwordState.ORE: swordValue = 1; break;
            case SwordState.MOLTEN: swordValue = 2; break;
            case SwordState.INGOT: swordValue = 3; break;
            case SwordState.FINISHED: swordValue = 4; break;
            default: swordValue = 0; break;
        }
        return swordValue;
    }
}
