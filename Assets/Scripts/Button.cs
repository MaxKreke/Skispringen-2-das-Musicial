using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool on;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public Material onMat;
    public Material offMat;

    private void Start()
    {
        on = false;
    }

    private void OnTriggerExit(Collider other)
    {
        on = false;
        one.GetComponent<MeshRenderer>().material = offMat;
        two.GetComponent<MeshRenderer>().material = offMat;
        three.GetComponent<MeshRenderer>().material = offMat;
        four.GetComponent<MeshRenderer>().material = offMat;
    }

    private void OnTriggerStay(Collider other)
    {
        on = true;
        one.GetComponent<MeshRenderer>().material = onMat;
        two.GetComponent<MeshRenderer>().material = onMat;
        three.GetComponent<MeshRenderer>().material = onMat;
        four.GetComponent<MeshRenderer>().material = onMat;
    }

}
