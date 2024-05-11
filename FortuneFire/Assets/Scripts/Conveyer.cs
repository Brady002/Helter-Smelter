using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour
{
    [SerializeField]
    private float forceAmount = 1f;
    [SerializeField]
    private float maxSpeed = 2f;

    private void OnTriggerStay(Collider other) 
    {
        if (other.TryGetComponent<IGrabable>(out IGrabable component))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
            Vector3 rotatedDir = rotation * transform.forward; //Rotated direction

            rb.AddForce(rotatedDir * forceAmount, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        
    }
}
