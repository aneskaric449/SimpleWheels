using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] WheelCollider leftFront, leftRear, rightFront, rightRear;
    [SerializeField] Transform leftFrontTransform, leftRearTransform, rightFrontTransform, rightRearTransform;

    public float acceleration = 400f;
    public float brakingForce = 200f;
    public float maxTurnAngle = 20f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle;

    private void FixedUpdate()
    {
        // Get the forward/reverse acceleration.
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        // Braking
        if(Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakingForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        // Apply acceleration to front wheels.
        leftFront.motorTorque = currentAcceleration;
        rightFront.motorTorque = currentAcceleration;

        // Apply braking for to all wheels.
        leftFront.brakeTorque = currentBrakeForce;
        leftRear.brakeTorque = currentBrakeForce;
        rightFront.brakeTorque = currentBrakeForce;
        rightRear.brakeTorque = currentBrakeForce;

        // Steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        leftFront.steerAngle = currentTurnAngle;
        rightFront.steerAngle = currentTurnAngle;

        // Update wheel meshes.
        UpdateWheels(leftFront, leftFrontTransform);
        UpdateWheels(leftRear, leftRearTransform);
        UpdateWheels(rightFront, rightFrontTransform);
        UpdateWheels(rightRear, rightRearTransform);

        void UpdateWheels(WheelCollider col, Transform trans)
        {
            // Get wheel collider state.
            Vector3 position;
            Quaternion rotation;
            col.GetWorldPose(out position, out rotation);

            // Apply rotation to wheels.
            trans.position = position;
            trans.rotation = rotation;
        }
    }
}
