using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Rigidbody body;
    public Vector3 initialPosition;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (transform.position.y < -5)
        {
            _Respawn();

        }
    }

    private void _Respawn()
    {
        Transform cam = transform.GetChild(0);
        if(cam.childCount > 0)
        {
            GameObject darm = cam.GetChild(0).gameObject;
            darm.GetComponent<Grapplendix>().CutLine();
        }
        body.velocity = Vector3.zero;
        transform.position = initialPosition;
        GameObject key = GetComponent<Inventory>().key;
        if (key)
        {
            key.SetActive(true);
            GetComponent<Inventory>().key = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kill")
        {
            _Respawn();
        }
    }
}
