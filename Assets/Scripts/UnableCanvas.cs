using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnableCanvas : MonoBehaviour
{
     
    public void unableCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
