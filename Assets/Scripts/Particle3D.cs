using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape3D
{
    count
}

public class Particle3D : MonoBehaviour
{
    /// <summary> The mass of the particle. </summary>
    public float mass;
    /// <summary> The local center of mass of the particle. </summary>
    public Vector3 centerOfMass;
    /// <summary> The moment of inertia of the particle. </summary>
    public float inertia;

    /// <summary>
    /// The position, velocity, and acceleration of our particle.
    /// These control the positional movement of the particle during the update.
    /// </summary>
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    /// <summary> The total sum of forces acting on the particle. </summary>
    public Vector3 force;

    /// <summary>
    /// The rotation, angular velocity, and angular acceleration of our particle.
    /// These control the rotation of the particle during the update.
    /// </summary>
    public Quaternion rotation;
    public Vector3 angularVelocity;
    public Vector3 angularAcceleration;

    /// <summary> The total sum of torque acting on the particle. </summary>
    public Vector3 torque;

    /// <summary>
    /// The method (either Explicit Euler or Kinematic) that has been selected for the particle's update
    /// </summary>
    public UpdateMethod updateMethod;

    // Start is called before the first frame update
    void Start()
    {
    }

    // FixedUpdate is called at a regular interval
    void FixedUpdate()
    {
        UpdateAcceleration();

        // Update the particle's position based on which method has been selected
        if (updateMethod == UpdateMethod.EXPLICIT_EULER)
        {
            UpdatePositionEulerExplicit(Time.fixedDeltaTime);
            UpdateRotationEulerExplicit(Time.fixedDeltaTime);
        }
        else if (updateMethod == UpdateMethod.KINEMATIC)
        {
            UpdatePositionKinematic(Time.fixedDeltaTime);
            UpdateRotationKinematic(Time.fixedDeltaTime);
        }
        // If something went wrong and we have an invalid UpdateMethod, error and return
        else
        {
            Debug.LogError("Invalid UpdateMethod: " + updateMethod);
            return;
        }

        // Update the particle's velocities
        UpdateVelocityEulerExplicit(Time.fixedDeltaTime);
        UpdateAngularVelocityEulerExplicit(Time.fixedDeltaTime);

        // Update the actual object's transform with our new values
        transform.position = position;
        transform.rotation = rotation;
    }

    // Step 2
    /// <summary>
    /// Update the particle's position according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdatePositionEulerExplicit(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t)dt
        // Euler's method
        // F(t+dt) = F(t) + f(t)dt
        //                + (dF/dt) * dt
        position += velocity * deltaTime;
    }

    /// <summary>
    /// Update the particle's position according to the kinematic formula.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdatePositionKinematic(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t) dt + 1/2 a(t) dt^2
        position += velocity * deltaTime + .5f * acceleration * deltaTime * deltaTime;
    }

    /// <summary>
    /// Update the particle's velocity according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdateVelocityEulerExplicit(float deltaTime)
    {
        // v(t+dt) = v(t) + a(t)dt
        velocity += acceleration * deltaTime;
    }

    // Step 2
    /// <summary>
    /// Update the particle's rotation according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdateRotationEulerExplicit(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t)dt
        // Euler's method
        // F(t+dt) = F(t) + f(t)dt
        //                + (dF/dt) * dt
        rotation *= ScaleQuaternion(deltaTime * 0.5f, Quaternion.Euler(angularVelocity) * rotation);
    }

    /// <summary>
    /// Update the particle's rotation according to the kinematic formula.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdateRotationKinematic(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t) dt + 1/2 a(t) dt^2
        rotation += angularVelocity * deltaTime + .5f * angularAcceleration * deltaTime * deltaTime;
    }

    /// <summary>
    /// Update the particle's angular velocity according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    private void UpdateAngularVelocityEulerExplicit(float deltaTime)
    {
        // v(t+dt) = v(t) + a(t)dt
        angularVelocity += angularAcceleration * deltaTime;
    }

    /// <summary>
    /// Update the particle's acceleration based on current forces.
    /// </summary>
    private void UpdateAcceleration()
    {
        // F = ma
        // a = f / m
        // a = 1 / m * f
        // a = .1 * m * f
        acceleration = (0.1f * mass) * force;
        force = Vector2.zero;
    }

    /// <summary>
    /// Apply a new force to the particle.
    /// </summary>
    /// <param name="newForce"> The force to add to the particle's current force. </param>
    public void ApplyForce(Vector2 newForce)
    {
        force += newForce;
    }

    private void CalculateMomentOfInertia()
    {

    }

    private void UpdateAngularAccelearation()
    {
        angularAcceleration = (1 / inertia) * torque;
    }

    public void ApplyTorque(Vector2 forcePoint, float forceAmount)
    {
        float momentArm = Vector2.Distance(centerOfMass, forcePoint);
        torque += momentArm * forceAmount;
    }

    public Quaternion ScaleQuaternion(float scalar, Quaternion quaternion)
    {
        throw new System.NotImplementedException();
    }
}
