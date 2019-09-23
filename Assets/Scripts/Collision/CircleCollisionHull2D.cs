using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollisionHull2D : CollisionHull2D
{

    public CircleCollisionHull2D() : base(CollisionHullType2D.CIRCLE) { }

    [Range(0.0f, 100.0f)]
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool TestCollisionVsCircle(CircleCollisionHull2D other)
    {
        // Collision passes if distance between centers <= sum of radii
        //  optimized collision passes if
        //   (distance between centers)^2 <= (sum of radii)^2
        // 1. Get particle centers   
        // 2. differentiate between centers
        // 3. distance squared = dot(diff, diff)  
        // 4. sum radii  
        // 5. square radii
        // 6. perfrom test: distSq <= sumSq



        return false;
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBoxHull2D other)
    {
        // calculate closest point by clamping circle center on each dimension
        // passes if closest point vs circle passes
        // 1. 

        return false;
    }

    public override bool TestCollisionVsOBB(ObjectBoundingBoxHull2D other)
    {
        // same as above, but first...
        // transform circle position by multiplying by box world matrix inverse
        // 1. 

        return false;
    }

}
