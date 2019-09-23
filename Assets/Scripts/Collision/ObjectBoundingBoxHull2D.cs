using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBoundingBoxHull2D : CollisionHull2D
{

    public ObjectBoundingBoxHull2D() : base(CollisionHullType2D.OBB) { }

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
        // see circle
        return other.TestCollisionVsOBB(this);
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBoxHull2D other)
    {
        // see aabb
        return other.TestCollisionVsOBB(this);
    }

    public override bool TestCollisionVsOBB(ObjectBoundingBoxHull2D other)
    {
        // AABB-OBB part 2 twice
        // 1. align A
        // 2. AABB-OBB A vs B
        // 3. align B
        // 4. AABB-OBB A vs B

        return false;
    }

}
