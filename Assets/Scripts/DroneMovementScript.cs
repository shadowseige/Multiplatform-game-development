using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementScript : MonoBehaviour
{
    Rigidbody ourDrone;

    private void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        rotation();
        ClampingSpeedValues();
        Swerwe();

        ourDrone.AddRelativeForce(Vector3.up * upForce);
        ourDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRoation, tiltAmountSideways));
    }

    public float upForce;
    void MovementUpDown()
    {
        if ((Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f))
        {
            if(Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K))
            {
                ourDrone.velocity = ourDrone.velocity;
            }
            if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            if(!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) || !Input.GetKey(KeyCode.L))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
            {
                upForce = 410;
            }
        }

        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        if (Input.GetKey(KeyCode.I))
        {
            upForce = 400;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.K))
        {
            upForce = -100;
        }
        else if(!Input.GetKey(KeyCode.I) && (!Input.GetKey(KeyCode.K)))
        {
            upForce = 98.1f;
        }
    }

    private float MovementForwardSpeed = 200.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;
    void MovementForward()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * MovementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.2f);
        }
    }

    private float wantedYRotation;
    [HideInInspector] public float currentYRoation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationVelocity;
    void rotation()
    {
        if (Input.GetKey(KeyCode.J))
        {
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.L))
        {
            wantedYRotation += rotateAmountByKeys;
        }

        currentYRoation = Mathf.SmoothDamp(currentYRoation, wantedYRotation, ref rotationVelocity, 0.25f);
    }

    private Vector3 velocityToSmoothDampZero;
    void ClampingSpeedValues()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampZero, 0.95f);
        }
    }

    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerwe()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal"))> 0.2f)
        {
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways,-20 * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);
        }
        else
        {
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
        }
    }
}
