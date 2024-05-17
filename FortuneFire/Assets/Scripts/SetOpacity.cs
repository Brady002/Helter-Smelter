using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOpacity : MonoBehaviour
{
    public List<GameObject> materials;

    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject GO in materials)
        {
            MeshRenderer mat = GO.GetComponent<MeshRenderer>();
            mat.material.color = new Vector4(mat.material.color.r, mat.material.color.g, mat.material.color.b, .3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject GO in materials)
        {
            MeshRenderer mat = GO.GetComponent<MeshRenderer>();
            mat.material.color = new Vector4(mat.material.color.r, mat.material.color.g, mat.material.color.b, 1);
        }
    }
}
