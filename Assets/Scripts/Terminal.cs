using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics.gravity = new Vector3(0, -9, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
