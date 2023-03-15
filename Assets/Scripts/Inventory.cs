using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject key;
    public GameObject money;

    private void Start()
    {
        key = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key" && !key)
        {
            this.GetComponent<Inventory>().key = other.gameObject;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Parent")
        {
            GameObject otherKey = other.gameObject.GetComponent<Inventory>().key;
            other.gameObject.GetComponent<Inventory>().key = key;
            key = otherKey;
        }

    }



}
