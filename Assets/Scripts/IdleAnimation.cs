using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public GameObject otherCamera;
    private MeshRenderer mr;
    private bool one = true;
    float idleDelay = .7f;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        InvokeRepeating("ChangeMaterial", idleDelay, idleDelay);
    }

    private void Update()
    {
        //Debug.Log("aaaa");
        Vector3 other = otherCamera.transform.position;
        Vector3 direction = (other - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        transform.Rotate(90*Vector3.right);
        transform.position = transform.parent.position;
    }

    private void ChangeMaterial()
    {
        if (one)
        {
            mr.material = mat1;
            one = false;
        }
        else
        {
            mr.material = mat2;
            one = true;
        }
    }


}
