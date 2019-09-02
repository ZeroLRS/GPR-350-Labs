using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    /// <summary>
    /// The position, velocity, and acceleration of our particle.
    /// These control the positional movement of the particle during the update.
    /// </summary>
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;

    /// <summary>
    /// The rotation, angular velocity, and angular acceleration of our particle.
    /// These control the rotation of the particle during the update.
    /// </summary>
    public float rotation;
    public float angularVelocity;
    public float angularAcceleration;

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
        // Step 3
        // Update the particle's position based on which method has been selected
        if (updateMethod == UpdateMethod.EXPLICIT_EULER)
        {
            UpdatePositionEulerExplicit(Time.fixedDeltaTime);
            UpdateRotationEulerExplicit(Time.fixedDeltaTime);
        }
        else if (updateMethod == UpdateMethod.KINEMATIC)
        {

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
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Step 4
        acceleration.y = -Mathf.Sin(Time.fixedTime);
        angularAcceleration = -Mathf.Sin(Time.fixedTime) * 180;
    }

    // Step 2
    /// <summary>
    /// Update the particle's position according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    public void UpdatePositionEulerExplicit(float deltaTime)
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
    public void UpdatePositionKinematic(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t) dt + 1/2 a(t) dt^2
        position += velocity * deltaTime + .5f * acceleration * deltaTime * deltaTime;
    }

    /// <summary>
    /// Update the particle's velocity according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    public void UpdateVelocityEulerExplicit(float deltaTime)
    {
        // v(t+dt) = v(t) + a(t)dt
        velocity += acceleration * deltaTime;
    }

    // Step 2
    /// <summary>
    /// Update the particle's rotation according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    public void UpdateRotationEulerExplicit(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t)dt
        // Euler's method
        // F(t+dt) = F(t) + f(t)dt
        //                + (dF/dt) * dt
        rotation += angularVelocity * deltaTime;
    }

    /// <summary>
    /// Update the particle's rotation according to the kinematic formula.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    public void UpdateRotationKinematic(float deltaTime)
    {
        // x(t+dt) = x(t) + v(t) dt + 1/2 a(t) dt^2
        rotation += angularVelocity * deltaTime + .5f * angularAcceleration * deltaTime * deltaTime;
    }

    /// <summary>
    /// Update the particle's angular velocity according to the explicit Euler function.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    public void UpdateAngularVelocityEulerExplicit(float deltaTime)
    {
        // v(t+dt) = v(t) + a(t)dt
        angularVelocity += angularAcceleration * deltaTime;
    }
}
