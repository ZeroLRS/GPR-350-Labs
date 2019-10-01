using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTestManager : MonoBehaviour
{

    public CollisionHull2D[] hulls;
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Keep track of how many times each hull collides for coloring
        int[] collisions = new int[hulls.Length];

        // Loop through all possible collisions
        for (int i = 0; i < hulls.Length; i++)
        {
            // j = i + 1 so we don't repeat collision checks
            for (int j = i+1; j < hulls.Length; j++)
            {
                if (CollisionHull2D.TestCollision(hulls[i], hulls[j]))
                {
                    collisions[i]++;
                    collisions[j]++;
                }
            }

            // For 0-2 collisions, use the material index
            if (collisions[i] < 3)
            {
                hulls[i].GetComponent<MeshRenderer>().material = materials[collisions[i]];
            }
            else // If there are three or more collisions, make it red 
            {
                hulls[i].GetComponent<MeshRenderer>().material = materials[3];
            }
        }



    }
}
