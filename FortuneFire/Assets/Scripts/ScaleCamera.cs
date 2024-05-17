using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCamera : MonoBehaviour
{

    private Camera cam;
    private float lastScreenWidth;
    private float lastScreenHeight;
    [SerializeField] public float targetSize = 29f;
    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraSize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateCameraSize();
        }
    }

    private void UpdateCameraSize()
    {
        float targetAspectRatio = Screen.width / (float)Screen.height;

        // Adjust the orthographic size based on the target height and current aspect ratio
        cam.orthographicSize = targetSize / 2f / targetAspectRatio;

        // Save the current screen dimensions
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }
}
