using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;
    private int fillTime; 
    private float timer = 0.0f;
    private bool isFilling = false;

    void Start()
    {
        progressBar.fillAmount = 0.0f;
    }

    void Update()
    {
        if (isFilling)
        {
            timer += Time.deltaTime;
            progressBar.fillAmount = timer / fillTime;

            if (progressBar.fillAmount >= 1.0f)
            {
                isFilling = false;
            }
        }
    }

    public void FillProgressBar(int time) {
        if (!isFilling) {
            timer = 0.0f;
            fillTime = time;
            isFilling = true;
        }
    }
}