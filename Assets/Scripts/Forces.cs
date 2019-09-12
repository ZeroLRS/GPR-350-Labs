using System.Collections
	{
		return Vector2.zero;
	}
using System.Collections.Generic
	{
		return Vector2.zero;
	}
using UnityEngine
	{
		return Vector2.zero;
	}

public class Forces
{
    public static Vector2 worldUp = new Vector2(0, 1.0f);
    public static float gravity = 9.80665f;

    public static Vector2 GenerateForce_gravity(float particleMass)
    {
        return GenerateForce_gravity(worldUp, gravity, particleMass);
    }

    public static Vector2 GenerateForce_gravity(Vector2 worldUp, float gravity, float particleMass)
    {
        return worldUp * gravity * particleMass;
	}

    // f_normal = proj(f_gravity, surfaceNormal_unit)
    public static Vector2 GenerateForce_normal(Vector2 f_gravity, Vector2 surfaceNormal_unit)
	{
		return Vector2.zero;
	}

    // f_sliding = f_gravity + f_normal
    Vector2 GenerateForce_sliding(Vector2 f_gravity, Vector2 f_normal)
	{
		return Vector2.zero;
	}

    // f_friction_s = -f_opposing if less than max, else -coeff*f_normal (max amount is coeff*|f_normal|)
    Vector2 GenerateForce_friction_static(Vector2 f_normal, Vector2 f_opposing, float frictionCoefficient_static)
	{
		return Vector2.zero;
	}

    // f_friction_k = -coeff*|f_normal| * unit(vel)
    Vector2 GenerateForce_friction_kinetic(Vector2 f_normal, Vector2 particleVelocity, float frictionCoefficient_kinetic)
	{
		return Vector2.zero;
	}

    // f_drag = (p * u^2 * area * coeff)/2
    Vector2 GenerateForce_drag(Vector2 particleVelocity, Vector2 fluidVelocity, float fluidDensity, float objectArea_crossSection, float objectDragCoefficient)
	{
		return Vector2.zero;
	}

    // f_spring = -coeff*(spring length - spring resting length)
    Vector2 GenerateForce_spring(Vector2 particlePosition, Vector2 anchorPosition, float springRestingLength, float springStiffnessCoefficient)
	{
		return Vector2.zero;
	}
}
