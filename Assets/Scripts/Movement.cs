using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool grounded = false;

    private float _jumpTimeoutDelta;

    public float jumpforce = 10;
    public float speed = 1;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    public GameObject otherCharacter;

    public Camera ownCamera;
    public Camera otherCamera;
    public Speedometer speedometer;

    private Rigidbody body;
    
    public bool isParent;

    public bool airMovement;
    public bool bouncyBoots;
    public bool doubleJump;

    private bool storedDoubleJump;
    private bool storedMoveCommand;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        storedDoubleJump = false;
        storedMoveCommand = false;
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        if(grounded|| airMovement || storedMoveCommand) Move();
        if (isParent) ClampSpeed();
        UpdateSpeedometer();
        SwitchCharacter();
    }

    private void JumpAndGravity()
    {
        if (bouncyBoots){
            Vector3 vel = body.velocity;
            if (Input.GetKey("space")&&grounded&&vel.y< 0)
            {
                body.velocity = new Vector3(vel.x, -vel.y*.9f, vel.z);
            }
        }
        if (grounded && Input.GetKeyDown("space"))
        {
            body.AddForce(Vector3.up* jumpforce, ForceMode.Impulse);
        }else if(storedDoubleJump && Input.GetKeyDown("space"))
        {
            Vector3 vel = body.velocity;
            body.velocity = new Vector3(vel.x, 0, vel.z);
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            //Double boost from air movement to make it more noticable tbh
            if(airMovement) body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            storedDoubleJump = false;
            storedMoveCommand = true;
        }
    }

    private void GroundedCheck()
    {
        grounded = Physics.CheckSphere(transform.position, .6f, GroundLayers);
        if (grounded) storedDoubleJump = doubleJump;
    }

    private void Move()
    {
        storedMoveCommand = false;
        float horizontalRotation = ownCamera.GetComponent<CameraRotation>().horizontalRotation;
        Vector3 force = Vector3.zero;
        switch (direction())
        {
            case 0:
                force = Vector3.forward * speed;
                break;
            case 1:
                force = (Vector3.forward+Vector3.right).normalized * speed;
                break;
            case 2:
                force = Vector3.right * speed;
                break;
            case 3:
                force = (Vector3.right + Vector3.back).normalized * speed;
                break;
            case 4:
                force = Vector3.back * speed;
                break;
            case 5:
                force = (Vector3.back + Vector3.left).normalized * speed;
                break;
            case 6:
                force = Vector3.left * speed;
                break;
            case 7:
                force = (Vector3.left + Vector3.forward).normalized * speed;
                break;
            default:
                break;
        }
        Vector3 dir = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * Vector3.forward;
        Vector3 rotatedForce = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * force;


        float currentSpeed = Vector3.ProjectOnPlane(body.velocity, Vector3.up).magnitude;
        float relativeSpeed = Vector3.Dot(rotatedForce, body.velocity);
        if (force != Vector3.zero)
        {
            body.velocity = Vector3.ClampMagnitude(rotatedForce.normalized * (currentSpeed+relativeSpeed)/2, 150)+body.velocity.y* Vector3.up;
            body.AddForce(rotatedForce);
        }
    }

    /*  
     *  -1: none
     *  0: forward
     *  1: diagonal
     *  2: right
     *  3: diagonal
     *  4: back
     *  5: diagonal
     *  6: left
     *  7: diagonal
    */
    private int direction()
    {
        bool forward = (Input.GetKey("w") || Input.GetKey("u") || Input.GetKey("up"));
        bool right = (Input.GetKey("d") || Input.GetKey("k") || Input.GetKey("right"));
        bool back = (Input.GetKey("s") || Input.GetKey("j") || Input.GetKey("down"));
        bool left = (Input.GetKey("a") || Input.GetKey("h") || Input.GetKey("left"));

        if (forward)
        {
            if (right && !left) return 1;
            if (!right && left) return 7;
            if (!back) return 0;
        }
        if (back)
        {
            if (right && !left) return 3;
            if (!right && left) return 5;
            if (!forward) return 4;
        }
        if (right && !left) return 2;
        if (!right && left) return 6;
        return -1;
    }

    public void SwitchCharacter()
    {
        if (Input.GetKeyDown("tab"))
        {
            ownCamera.enabled = false;
            otherCamera.enabled = true;
            otherCharacter.GetComponent<Movement>().enabled = true;
            GetComponent<Movement>().enabled = false;
        }
    }

    private void ClampSpeed()
    {
        Vector3 velocity = body.velocity;
        Vector3 modifiedVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
        float ratio = modifiedVelocity.magnitude / 10;
        if(ratio > 1) modifiedVelocity/=ratio;
        modifiedVelocity = new Vector3(modifiedVelocity.x, velocity.y, modifiedVelocity.z);
        body.velocity = modifiedVelocity;
    }

    private void UpdateSpeedometer()
    {
        speedometer.SetSpeed(Vector3.ProjectOnPlane(body.velocity, Vector3.up).magnitude);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Jump")
        {
            Vector3 direction = other.GetComponent<JumpPad>().direction;
            body.velocity = direction;
        }
    }

}
