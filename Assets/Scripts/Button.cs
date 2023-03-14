using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool on;
    private void Start()
    {
        on = false;
    }

    private void OnTriggerExit(Collider other)
    {
        on = false;
    }

    private void OnTriggerStay(Collider other)
    {
        on = true;
    }

}
