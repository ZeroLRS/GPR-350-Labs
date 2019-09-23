using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHull2D : MonoBehaviour
{

    public enum CollisionHullType2D
    {
        CIRCLE = 0,
        AABB,
        OBB,
        count
    }

    private CollisionHullType2D type { get; }

    protected CollisionHull2D(CollisionHullType2D type_set)
    {
        type = type_set;
    }

    protected Particle2D particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<Particle2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool TestCollision(CollisionHull2D a, CollisionHull2D b)
    {
        // call the approprite collision test

        return false;
    }

    public abstract bool TestCollisionVsCircle(CircleCollisionHull2D other);
    public abstract bool TestCollisionVsAABB(AxisAlignedBoundingBoxHull2D other);
    public abstract bool TestCollisionVsOBB(ObjectBoundingBoxHull2D other);
}
