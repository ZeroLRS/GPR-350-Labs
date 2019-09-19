using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Forces
{
    private static Vector2 worldUp = new Vector2(0, 1.0f);
    private static float gravity = -9.80665f;

    /// <summary> Standard gravity on earth </summary>
    public static Vector2 GenerateForce_gravity(float particleMass)
    {
        return GenerateForce_gravity(worldUp, gravity, particleMass);
    }

    /// <summary> f_gravity = mg </summary> 
    public static Vector2 GenerateForce_gravity(Vector2 worldUp, float gravity, float particleMass)
    {
        return worldUp * gravity * particleMass;
	}

    /// <summary> f_normal = proj(f_gravity, surfaceNormal_unit) </summary> 
    public static Vector2 GenerateForce_normal(Vector2 f_gravity, Vector2 surfaceNormal_unit)
	{
        // Unity has projection for V3s, but not V2s, so we convert them
        Vector3 f_gravity3 = -f_gravity;
        Vector3 surfaceNormal_unit3 = surfaceNormal_unit;

        // Run the projection
        Vector3 projection = Vector3.Project(f_gravity3, surfaceNormal_unit);

        return new Vector2(projection.x, projection.y);
	}

    /// <summary> f_sliding = f_gravity + f_normal </summary> 
    public static Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
	{
        return f_gravity + f_normal;
	}

    /// <summary> f_friction_s = -f_opposing if less than max, else -coeff*f_normal (max amount is coeff*|f_normal|) </summary>
    public static Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
	{
        float f_normal_abs = f_normal.magnitude;
        float max = frictionCoefficient_static * f_normal_abs;
        
        if (f_opposing.magnitude < max)
        {
            return -f_opposing;
        }
        else
        {
            return -frictionCoefficient_static * f_normal;
        }
	}

    /// <summary> f_friction_k = -coeff*|f_normal| * unit(vel) </summary> 
    public static Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 particleVelocity, float frictionCoefficient_kinetic)
	{
        //Vector2 f_normal_abs = new Vector2(Mathf.Abs(f_normal.x), Mathf.Abs(f_normal.y));
        float f_normal_abs = f_normal.magnitude;

        return -frictionCoefficient_kinetic * f_normal_abs * particleVelocity.normalized;
	}

    /// <summary> f_drag = (p * u^2 * area * coeff)/2 </summary>
    public static Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
	{
        Vector2 dragAmount = (particleVelocity * particleVelocity.magnitude * fluidDensity * objectArea_crossSection * objectDragCoefficient) * .5f;

        return fluidVelocity * dragAmount;
	}

    /// <summary> f_spring = -coeff*(spring length - spring resting length) </summary> 
    public static Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosition, float springRestingLength, float springStiffnessCoefficient)
	{
        Vector2 difference = particlePosition - anchorPosition;
        float springLength = Vector2.Distance(particlePosition, anchorPosition);
        float forceAmount = -springStiffnessCoefficient * (springLength - springRestingLength);

        return difference * forceAmount / springLength;
	}
}
