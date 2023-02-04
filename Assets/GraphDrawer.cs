using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    private float inputA = 0;

    void Start()
    {

    }

    public void UpdateInputA(string a)
    {
        if (float.TryParse(a, out inputA))
        {
            Debug.Log(inputA);
        }
        else
        {
            Debug.Log("UpdateInputA couldn't parse a string...");
        }
    }

}
