using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplendix : MonoBehaviour
{
    private Vector3 target;
    private bool grappled = false;
    private Rigidbody body;
    public bool grapplendix = true;
    public Camera ownCamera;
    public LayerMask boxLayers;
    public GameObject parentChild;
    private SpringJoint joint;


    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!grapplendix) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, ownCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, boxLayers))
            {
                grappled = true;
                target = hit.point;
                //joint = player.gameObject.AddComponent<SpringJoint>();

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            target = Vector3.zero;
            grappled = false;
        }
        if (grappled)
        {
            Vector3 direction = (target-transform.position).normalized;

            //Limit speed
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(body.velocity, Vector3.up);
            body.velocity = Vector3.ClampMagnitude(horizontalVelocity, 150) + Vector3.ClampMagnitude(body.velocity.y * Vector3.up,75);

            body.AddForce(direction);
        }
    }
}
