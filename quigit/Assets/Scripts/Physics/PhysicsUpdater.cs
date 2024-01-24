using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Velocity")]
    public Vector3 globalVelocity;

    [Header("Physics settings")]
    public float gravityStrength = 10;
    public int maxBounces = 3;
    public float skinWidth = 0.01f;

    private void Update()
    {
        //globalVelocity.y -= gravityStrength * Time.deltaTime;
        Vector3 finalVel = globalVelocity * Time.deltaTime;
        finalVel = CollideAndSlide(finalVel, transform.position, 0, false, finalVel);
        globalVelocity = finalVel / Time.deltaTime;
        globalVelocity = manageGravity(globalVelocity);
        transform.position += globalVelocity * Time.deltaTime;
    }

    private Vector3 CollideAndSlide(Vector3 vel, Vector3 pos, int depth, bool gravityPass, Vector3 velInit) //stole this from https://www.youtube.com/watch?v=YR6Q7dUz2uk
    {                                                                                                            // highly recomend watching it
        if (depth >= maxBounces)
        { 
            return Vector3.zero;
        }

        float dist = vel.magnitude + skinWidth;

        RaycastHit hit;

        if (Physics.SphereCast(pos, 0.5f, vel, out hit, dist))
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);
            Vector3 leftover = vel - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if (snapToSurface.magnitude <= skinWidth)
            {
                snapToSurface = Vector3.zero;
            }
            if (angle <= 90)
            {
                /*if (gravityPass) *///return snapToSurface;
                leftover = ProjectAndScale(leftover, hit.normal);
            }
            else
            {
                float scale = 1 - Vector3.Dot(new Vector3(hit.normal.x, 0, hit.normal.z).normalized, -new Vector3(velInit.x, 0, velInit.z).normalized);

                if (true && !gravityPass)
                {
                    leftover = ProjectAndScale(new Vector3(leftover.x, 0, leftover.z), new Vector3(hit.normal.x, 0, hit.normal.z));
                    leftover *= scale;
                }
                else
                {
                    leftover = ProjectAndScale(leftover, hit.normal) * scale;
                }

            }

            return snapToSurface + CollideAndSlide(leftover, pos + snapToSurface, depth + 1, gravityPass, velInit);
        }

        return vel;
    }

    private Vector3 ProjectAndScale(Vector3 vec, Vector3 normal)   //helper functions bellow
    {
        float mag = vec.magnitude;
        vec = Vector3.ProjectOnPlane(vec, normal).normalized;
        vec *= mag;
        return vec;
    }


    public Vector3 manageGravity(Vector3 vel)
    {
        float height = vel.y;
        float goodHeight = height -= gravityStrength * Time.deltaTime;
        float convFactor = goodHeight / height;
        return vel * convFactor;
    }
}