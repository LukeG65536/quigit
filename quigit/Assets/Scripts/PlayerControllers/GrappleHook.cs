using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GrappleHook : MonoBehaviour
{
    [Header("refs")]
    public Transform camPos;
    public PhysicsUpdater physics;

    [Header("settings")]
    public float strength = 1f;
    public float maxStrength = 200f;
    public float grappleCooldown = 1f;
    public float grappleDelay = 0.1f;
    public LayerMask grappleAble;

    [Header("state")]
    public bool canGrapple = true;
    public bool isInGrapple = false;
    public Vector3 grapplePoint;
    public float grappleLength;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            StartCoroutine(startGrapple());

            Debug.Log("Starting grapple");
        }

        if (Input.GetMouseButtonUp(2))
        {
            isInGrapple = false;
        }

        if (isInGrapple)
        {
            grappleUpdate();
        }
    }

    public void grappleUpdate()
    {
        Vector3 snapToHit = grapplePoint - camPos.position;
        float dist = snapToHit.magnitude;
        float legthFromMaxCircle = grappleLength - dist;
        float forceStrengthUnfiltered = 1 / (legthFromMaxCircle+1);
        float finalStrength = Mathf.Clamp(forceStrengthUnfiltered,0f,maxStrength);
        if (finalStrength == 0) finalStrength = maxStrength; 
        Vector3 force = snapToHit.normalized * finalStrength;
        Debug.Log(finalStrength);
        physics.globalVelocity += force * Time.deltaTime;
    }

    IEnumerator startGrapple()
    {
        yield return new WaitForSeconds(grappleDelay);
        if(Physics.Raycast(camPos.position, camPos.forward, out RaycastHit hit, 100000f, grappleAble))
        {
            Vector3 snapToHit = hit.point - camPos.position;
            grappleLength = snapToHit.magnitude;

            grapplePoint = hit.point;
            isInGrapple = true;
            Debug.Log("grapple started");
            StartCoroutine(startCooldown());
        }
    }

    IEnumerator startCooldown()
    {
        yield return new WaitForSeconds(grappleCooldown);
        canGrapple = true;
    }
}
