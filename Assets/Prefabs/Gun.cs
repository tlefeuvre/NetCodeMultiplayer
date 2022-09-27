using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Gun : NetworkBehaviour
{
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCamera;
    public Transform attackPoint;
    public ParticleSystem particles;
    public Vector3 aimDirection;
    public bool allowInvoke = true;
    private void Awake()
    {
       
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }
    private void Update()
    {
        if (IsOwner)
        {
            particles.Stop();

            MyInput();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Cursor.visible = !Cursor.visible;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }


    }
    private void MyInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            readyToShoot = false;
            bulletsLeft--;
            bulletsShot++;

            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("RAYCAST yes");
                targetPoint = hit.point;

            }
            else
            {
                Debug.Log("RAYCAST NO");
                targetPoint = ray.GetPoint(75);

            }

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            aimDirection = directionWithoutSpread;
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            if (IsHost)
            {
                Debug.Log("shoot");
                particles.Play();

                Shoot(directionWithoutSpread);
                if (allowInvoke)
                {
                    Invoke("ResetShot", timeBetweenShooting);
                    allowInvoke = false;
                }
                if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
                {
                    Invoke("Shoot", timeBetweenShooting);
                }
            }
            else {
                Debug.Log("submit request  shoot");
                particles.Play();

                SubmitRequestShotServerRpc(directionWithoutSpread);
                if (allowInvoke)
                {
                    Invoke("ResetShot", timeBetweenShooting);
                    allowInvoke = false;
                }
                if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
                {
                    Invoke("Shoot", timeBetweenShooting);
                }
            }
        }

    }
    [ServerRpc]
    private void SubmitRequestShotServerRpc(Vector3 directionWithoutSpread, ServerRpcParams rpcParams = default)
    {
        Debug.Log("in submit request");

        Shoot(directionWithoutSpread);
    }
    private void Shoot(Vector3 directionWithoutSpread)
    {
        Debug.Log("Shhot");

       


        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.GetComponent<NetworkObject>().Spawn();

        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse);



    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", timeBetweenShooting);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
