using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision
{
    public CollisionHull2D a, b;
    public bool collision;
    public Vector2 location;
    public Vector2 normal;
    public Vector2 restitution;
    public float collisionDepth;

    public Collision(CollisionHull2D firstCollider, CollisionHull2D secondCollider)
    {
        a = firstCollider;
        b = secondCollider;

        if (CollisionHull2D.TestCollision(a, b))
        {
            collision = true;
            // ****TODO: calculate the rest of the needed info
        }
    }
}
