using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public bool canShoot = true;
    public CameraController cameraController;
    public PhysicsUpdater physics;

    public float blastRadius = 10f;

    public float strength = 1f;

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Input.GetMouseButton(0))
        {
            shoot();
            StartCoroutine(shootCooldown(2f));
        }
    }

    private void shoot()
    {
        if (Physics.Raycast(cameraController.transform.position, cameraController.transform.forward, out RaycastHit hit, blastRadius))
        {
            canShoot = false;
            float dist = hit.distance;
            Vector3 dir = (cameraController.transform.position - hit.point).normalized;
            float power = blastRadius - dist;

            dir *= power;

            physics.globalVelocity += dir * strength;
        }
    }

    private IEnumerator shootCooldown(float sec)
    {
        yield return new WaitForSeconds(sec);

        canShoot = true;
    }
}
