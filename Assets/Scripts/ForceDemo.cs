using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceDemos
{
    GRAVITY = 0,
    KINETIC_FRICTION,
    STATIC_FRICTION,
    NORMAL,
    SPRING,
    DRAG,
    count
}

public class ForceDemo : MonoBehaviour
{
    public Particle2D particle;
    public GameObject ramp, shallowRamp, springAnchor;
    public ForceDemos demo;

    // Start is called before the first frame update
    void Start()
    {
        if (demo == ForceDemos.NORMAL)
        {
            ramp.SetActive(true);
        }

        if (demo == ForceDemos.SPRING)
        {
            springAnchor.SetActive(true);
        }

        if (demo == ForceDemos.STATIC_FRICTION || demo == ForceDemos.KINETIC_FRICTION)
        {
            shallowRamp.SetActive(true);
        }

        if (demo == ForceDemos.DRAG
            || demo == ForceDemos.SPRING
            || demo == ForceDemos.KINETIC_FRICTION
            || demo == ForceDemos.STATIC_FRICTION)
        {
            particle.velocity = new Vector2(1.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newForce = Vector2.zero;

        if (demo == ForceDemos.GRAVITY)
        {
            newForce = Forces.GenerateForce_gravity(particle.mass);
        }
        else if (demo == ForceDemos.NORMAL)
        {
            newForce = Forces.GenerateForce_gravity(particle.mass);
            newForce = Forces.GenerateForce_normal(newForce, new Vector2(-.5f, 0.5f));
        }
        else if (demo == ForceDemos.DRAG)
        {
            newForce = Forces.GenerateForce_drag(particle.velocity,
                new Vector2(-1.0f, 0.0f),
                .5f, 1.0f, 5.0f);
        }
        else if (demo == ForceDemos.SPRING)
        {
            newForce = Forces.GenerateForce_spring(particle.position,
                new Vector2(0.0f, 2.0f), 1, 5f);

            newForce += Forces.GenerateForce_gravity(particle.mass);
        }
        else if (demo == ForceDemos.KINETIC_FRICTION)
        {
            newForce = Forces.GenerateForce_friction_kinetic(new Vector2(-.3f, 0.0f),
                particle.velocity, 1.0f);
        }
        else if (demo == ForceDemos.STATIC_FRICTION)
        {
            newForce = Forces.GenerateForce_friction_static(new Vector2(.3f, 0.0f),
                particle.velocity,
                1.0f);
        }

        Debug.Log("Force Applied: " + newForce.ToString("F4"));
        particle.ApplyForce(newForce);
    }
}
