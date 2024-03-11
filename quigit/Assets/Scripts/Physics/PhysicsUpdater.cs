using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhysicsUpdater : MonoBehaviour
{
    public bool frozen = false;

    // Start is called before the first frame update
    [Header("Velocity")]
    public Vector3 globalVelocity;

    [Header("Physics settings")]
    public bool xLock = false;
    public float gravityStrength = 10;
    public int maxBounces = 4;
    public float skinWidth = 0.01f;
    public LayerMask stuff;

    public float fpsApprox;

    [Header("refs")]
    public TextMeshProUGUI fpsText;
    private bool activeGrapple;

    private void Update()
    {
        managePhysics(frozen);

        fpsApprox = 1 / Time.smoothDeltaTime;

        fpsText.text = fpsApprox.ToString();
    }

    private void managePhysics(bool freeze)
    {

        if(!freeze) globalVelocity.y -= gravityStrength * Time.deltaTime;

        applyPhysics(false);                                       //apply gravity and basic colisions

        if (!xLock) return;                                //if xLocked is false then were done here

        Vector3 localVel = transform.InverseTransformDirection(globalVelocity);    //translate global to local
        localVel.x = 0;                                                            //set local x vel to 0 for surf
        globalVelocity = transform.TransformDirection(localVel);                    //translate back to global
        applyPhysics(true);                                                       //apply phisics but this time preserve vel
    }

    public void applyPhysics(bool keepVel)
    {
        Vector3 finalVel = globalVelocity * Time.deltaTime;
        finalVel = CollideAndSlide(finalVel, transform.position, 0, keepVel);
        transform.position += finalVel;
        globalVelocity = finalVel / Time.deltaTime;
    }

    private Vector3 CollideAndSlide(Vector3 vel, Vector3 pos, int depth, bool keepVel) //stole this from https://www.youtube.com/watch?v=YR6Q7dUz2uk
    {                                                                                                            // highly recomend watching it
        if (depth >= maxBounces)
        { 
            return Vector3.zero;      //if max bounces was hit return 0
        } 

        float dist = vel.magnitude + skinWidth;

        RaycastHit hit;

        if (Physics.SphereCast(pos, 0.5f, vel, out hit, dist, stuff))     //sphere cast of sphere
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);    // this is the velocity that will move you all the way up to the wall
            Vector3 leftover = vel - snapToSurface;                                      //whats leftover after the wall snap
            leftover = Vector3.ProjectOnPlane(leftover, hit.normal);                  //project that onto the plane of the wall

            if (keepVel)                                                  //if snap is on, no velocity is can be lost unless max bounces
            {
                leftover = vel - snapToSurface;
                float mag = leftover.magnitude;  //store length of leftover
                leftover = Vector3.ProjectOnPlane(leftover, hit.normal).normalized * mag;   //project on plane but have it keep its  vel
            }

            return snapToSurface + CollideAndSlide(leftover, pos + snapToSurface, depth + 1, keepVel); //start the function again with the leftover to make sure
        }

        return vel;
    }


    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        Vector3 velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);

        globalVelocity = velocityToSet;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = gravityStrength;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}