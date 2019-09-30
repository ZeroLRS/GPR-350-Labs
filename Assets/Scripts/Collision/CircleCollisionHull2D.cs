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
        
        Vector2 otherPos = other.particle.position;

        float distSq = Vector2.Dot(particle.position, otherPos);

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

        Vector2 closestPoint = Vector2.zero;

        // Closest point on x-axis
        if (pos.x < otherPos.x)
        {
            // Circle is to the left of the box, so we use the left side of the box as our X
            closestPoint.x = otherPos.x - otherDims.x * .5f;
        }
        else if (pos.x > otherPos.x + otherDims.x)
        {
            // Circle is to the right of the box, so we use the right side of the box as our X
            closestPoint.x = otherPos.x + otherDims.x * .5f;
        }
        else
        {
            // If both of the above cases fail, the x-axis of the center of the circle is inside of the box, so the position is the closest position
            closestPoint.x = pos.x;
        }

        // Closest point on y-axis
        if (pos.y < otherPos.y)
        {
            // Circle is to the left of the box, so we use the left side of the box as our Y
            closestPoint.y = otherPos.y - otherDims.y * .5f;
        }
        else if (pos.y > otherPos.y + otherDims.y)
        {
            // Circle is to the right of the box, so we use the right side of the box as our Y
            closestPoint.y = otherPos.y + otherDims.y * .5f;
        }
        else
        {
            // If both of the above cases fail, the y-axis of the center of the circle is inside of the box, so the position is the closest position
            closestPoint.y = pos.y;
        }

        // If the box's closest point is inside the circle's radius, there is collision
        float distSq = Vector2.Dot(pos, closestPoint);
        float radiusSq = radius * radius;

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
