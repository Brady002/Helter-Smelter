using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in");
        if(other.GetComponent<PlayerController>())
        {
            Debug.Log("out");
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.TakeDamage(Vector3.zero, 4f);
            pc.transform.position = respawnPoint.transform.position;
        }
    }
}
