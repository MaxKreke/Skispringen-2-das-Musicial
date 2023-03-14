using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Vector3 direction;
    private Vector3 storedDirection;
    public Button button;
    private Renderer renderer;
    public Material on;
    public Material off;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        storedDirection = direction;
    }

    private void Update()
    {
        if (button.on)
        {
            direction = storedDirection;
            renderer.material = on;
        }
        else
        {
            direction = Vector3.zero;
            renderer.material = off;
        }
    }

}
