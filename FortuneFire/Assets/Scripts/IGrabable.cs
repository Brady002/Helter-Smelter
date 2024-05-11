using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable
{
    public Vector3 Position { get; }

    public delegate void GrabEvent(Transform AttachPoint);
    public event GrabEvent OnGrabObject;

    public delegate void PutDownEvent(Transform AttachPoint);
    public event PutDownEvent OnPutDown;

    public void PickUp(Transform Position);

}
