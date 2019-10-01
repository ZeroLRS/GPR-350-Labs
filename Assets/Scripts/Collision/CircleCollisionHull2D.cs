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
        // Make the radius match up with Unity's scaling
        radius *= 0.5f;
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
        
        Vector2 otherPos = other.particle.position;

        Vector2 diff = particle.position - otherPos;
        float distSq = Vector2.Dot(diff, diff);

        float sumSq = radius + other.radius;
        sumSq *= sumSq;

        if (distSq <= sumSq)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBoxHull2D other)
    {
        // calculate closest point by clamping circle center on each dimension
        // passes if closest point vs circle passes

        Vector2 pos = particle.position;
        Vector2 otherPos = other.particle.position;
        Vector2 otherDims = other.dimensions;

        // Because we are doing math from the center of the box, we need half of each dimension
        otherDims *= 0.5f;

        Vector2 closestPoint = Vector2.zero;

        closestPoint.x = Mathf.Clamp(pos.x, otherPos.x - otherDims.x, otherPos.x + otherDims.x);
        closestPoint.y = Mathf.Clamp(pos.y, otherPos.y - otherDims.y, otherPos.y + otherDims.y);

        Debug.Log("Closest point on " + other.gameObject.name + " to " + gameObject.name +" is: " + closestPoint);

        // If the box's closest point is inside the circle's radius, there is collision
        Vector2 diff = pos - closestPoint;
        float distSq = Vector2.Dot(diff, diff);
        float radiusSq = radius * radius;
        Debug.Log("Diff: " + diff);
        Debug.Log("distSq: " + distSq);
        Debug.Log("radiusSq: " + radiusSq);


        // Using square distance and radius for less complex calculation
        if (distSq <= radiusSq)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool TestCollisionVsOBB(ObjectBoundingBoxHull2D other)
    {
        // same as above, but first...
        // transform circle position by multiplying by box world matrix inverse
        
        return false;
    }

}
