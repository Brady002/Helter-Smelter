using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("Title");
    }
}