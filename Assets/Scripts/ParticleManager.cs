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
    public enum ParticleMotion
    {
        SINE,
        FORWARD,
        FORWARD_ROTATION,
        count
    }

    [System.Serializable]
    class ParticleWrapper
    {
        public Particle2D particle;
        public ParticleMotion motion;
        public float remainingDuration;
        public float spawnTime;
    }

    public UpdateMethod updateMethod;

    public float particleDuration = 3f;

    public GameObject particlePrefab;

    [SerializeField]
    private List<ParticleWrapper> particles;

    private bool updateMethodChanged;

    // Start is called before the first frame update
    void Start()
    {
        particles = new List<ParticleWrapper>();
    }

    // Update is called once per frame
    void Update()
    {
        // Swap update methods
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (updateMethod == UpdateMethod.EXPLICIT_EULER)
            {
                updateMethod = UpdateMethod.KINEMATIC;
            }
            else if (updateMethod == UpdateMethod.KINEMATIC)
            {
                updateMethod = UpdateMethod.EXPLICIT_EULER;
            }

            updateMethodChanged = true;

            Debug.Log("Chaning update method to: " + updateMethod);
        }

        // Spawn a particle in a sine motion
        if (Input.GetKey(KeyCode.S))
        {
            GameObject particleObj = GameObject.Instantiate(particlePrefab);
            Particle2D newParticle = particleObj.GetComponent<Particle2D>();

            newParticle.velocity = new Vector2(1.0f, 1.0f);
            newParticle.updateMethod = updateMethod;

            ParticleWrapper particle = new ParticleWrapper();

            particle.particle = newParticle;
            particle.motion = ParticleMotion.SINE;
            particle.remainingDuration = particleDuration;
            particle.spawnTime = Time.fixedTime;

            particles.Add(particle);
        }

        // Spawn a particle in a forward motion
        if (Input.GetKey(KeyCode.F))
        {
            GameObject particleObj = GameObject.Instantiate(particlePrefab);
            Particle2D newParticle = particleObj.GetComponent<Particle2D>();

            newParticle.velocity = new Vector2(1.0f, 0.0f);
            newParticle.updateMethod = updateMethod;

            ParticleWrapper particle = new ParticleWrapper();

            particle.particle = newParticle;
            particle.motion = ParticleMotion.FORWARD;
            particle.remainingDuration = particleDuration;
            particle.spawnTime = Time.fixedTime;

            particles.Add(particle);
        }
    }

    void FixedUpdate()
    {
        // Update the specific physics for each particle
        for (int i = 0; i < particles.Count; i++)
        {
            Particle2D particle = particles[i].particle;

            if (updateMethodChanged)
            {
                particle.updateMethod = updateMethod;
                updateMethodChanged = false;
            }

            if (particles[i].motion == ParticleMotion.SINE)
            {
                // Part 4
                particle.acceleration.y = -Mathf.Sin(Time.fixedTime - particles[i].spawnTime);
            }

            particles[i].remainingDuration -= Time.fixedDeltaTime;
            //Debug.Log("Remaining duration: " + particles[i].remainingDuration);

            if (particles[i].remainingDuration <= 0)
            {
                Destroy(particle.gameObject);
                particles.Remove(particles[i]);
                i--;
            }
        }
    }
}
