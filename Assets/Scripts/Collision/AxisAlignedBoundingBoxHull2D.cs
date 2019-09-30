using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAlignedBoundingBoxHull2D : CollisionHull2D
{

    public AxisAlignedBoundingBoxHull2D() : base(CollisionHullType2D.AABB) { }

    /// <summary> The dimensions of the box. </summary>
    public Vector2 dimensions;

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
        return other.TestCollisionVsAABB(this);
    }

    public override bool TestCollisionVsAABB(AxisAlignedBoundingBoxHull2D other)
    {
        // for each dimesion, max extent of A greater than min extent of B
        // 1. A.X < B.X
        // 2. A.Y < B.Y
        // 3. B.X < A.X
        // 4. B.Y < A.Y

        return false;
    }

    public override bool TestCollisionVsOBB(ObjectBoundingBoxHull2D other)
    {
        // same as above twice
        //  first, test AABB vs max extents of OBB
        //  then, multiply by OBB inverse matrix, do test again
        // 1. 

        return false;
    }

}
