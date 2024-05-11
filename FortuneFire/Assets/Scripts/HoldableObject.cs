using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour, IGrabable
{
    public Vector3 _Position;

    public Vector3 Position { get => _Position; private set => _Position = value; }

    public event IGrabable.GrabEvent OnGrabObject;
    public event IGrabable.PutDownEvent OnPutDown;

    private void Start()
    {
        Position = transform.position;
    }

    public void PickUp(Transform Attach)
    {
        transform.position = Attach.position;
    }
}
