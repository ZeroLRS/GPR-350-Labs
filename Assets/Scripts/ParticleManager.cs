using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpdateMethod
{
    EXPLICIT_EULER,
    KINEMATIC,
    count
}

public class ParticleManager : MonoBehaviour
{
    public UpdateMethod updateMethod;

    public float particleDuration = 3f;

    [SerializeField]
    private List<Particle2D> particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = new List<Particle2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
