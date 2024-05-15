using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public List<Animator> platforms;
    private bool[,] platLayout = new bool[5, 3]
    {
        //Layouts of the platforms activation
        {true, true, true},
        {true, false, true},
        {false, true, false},
        {true, false, false},
        {false, false, true},
    };
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateLayout), 5f, 20f);
    }

    private void UpdateLayout()
    {
        int layout = Random.Range(0, platLayout.GetLength(0));
        for(int i = 0; i < platLayout.GetLength(1); i++)
        {
            platforms[i].SetBool("Extended", platLayout[layout, i]);
        }
        
    }
}
