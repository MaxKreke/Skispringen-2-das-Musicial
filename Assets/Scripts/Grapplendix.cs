using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplendix : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 target;
    private bool grappled = false;
    private Rigidbody body;
    public bool grapplendix = true;
    public Camera ownCamera;
    public LayerMask boxLayers;
    public GameObject parentChild;
    private SpringJoint joint;
    private float maximalDistanz = 300f;


    private void Start()
    {
        body = parentChild.GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {

        if (!grapplendix) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, ownCamera.transform.TransformDirection(Vector3.forward), out hit, maximalDistanz, boxLayers))
            {
                grappled = true;
                target = hit.point;
                joint = parentChild.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = target;

                float distanceFromPoint = Vector3.Distance(parentChild.transform.position, target);

                joint.maxDistance = distanceFromPoint * 0.8f;
                joint.minDistance =  0.25f;
                joint.spring = 4.5f;
                joint.damper = 7.0f;
                joint.massScale = 4.5f;

                lr.positionCount = 2;

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CutLine();
        }
        if (grappled)
        {
            //Debug.Log("ye");
            //Vector3 direction = (target-transform.position).normalized;

            ////Limit speed
            //Vector3 horizontalVelocity = Vector3.ProjectOnPlane(body.velocity, Vector3.up);
            //body.velocity = Vector3.ClampMagnitude(horizontalVelocity, 150) + Vector3.ClampMagnitude(body.velocity.y * Vector3.up,75);

            //body.AddForce(direction);
        }
    }

    private void LateUpdate()
    {
        drawRope();

    }

    private void drawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target);
    }

    public void CutLine()
    {
        lr.positionCount = 0;
        Destroy(joint);
        target = Vector3.zero;
        grappled = false;
    }



}
