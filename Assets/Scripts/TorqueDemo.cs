using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueDemo : MonoBehaviour
{
    public Shape shapeDemo;
    public Particle2D particle;

    public GameObject rod, disc, rectangle;

    // Start is called before the first frame update
    void Start()
    {
        if (shapeDemo == Shape.ROD)
        {
            rod.SetActive(true);
        }
        else if (shapeDemo == Shape.DISK)
        {
            disc.SetActive(true);
        }
        else if (shapeDemo == Shape.RECTANGLE)
        {
            rectangle.SetActive(true);
        }

        particle.CalculateMomentOfInertia();
    }

    // Update is called once per frame
    void Update()
    {
        particle.ApplyTorque(new Vector2(0, 1.0f), 1.0f);
    }
}
