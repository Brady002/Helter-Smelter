using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableConveyer : MonoBehaviour
{
    [SerializeField] public Vector3 direction = Vector3.forward;
    [SerializeField] public float force = 10f;
    private bool isOn;
    private float timer;

    [Header("Material")]
    public Material mat;
    private double matAnimationSpeed = 1;

    private void Update()
    {
        if(force <= .1)
        {
            isOn = false;
        } else
        {
            isOn = true;
        }

        timer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if(isOn)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(direction * force, ForceMode.Force);
        }
    }

    private IEnumerator ChangeDirection(float pauseTime, Vector3 newDirection, float newForce)
    {
        float _force = force;
        timer = 0;
        force = Mathf.Lerp(newForce, 0, timer);
        //mat.Property
        yield return new WaitForSeconds(pauseTime);
        timer = 0;
        direction = newDirection;
        force = 0;
        force = Mathf.Lerp(0, newForce, timer);
       

    }
}
