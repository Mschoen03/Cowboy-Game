using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    public TextMeshProUGUI text;
    public GameObject bulletHoleGraphic;
    public GameObject muzzleFlash;



    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();


        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //RayCast
        GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity, attackPoint);
        Destroy(flash, 0.1f);

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            Debug.Log("Hit: " + rayHit.collider.name);

            bool spawnBulletHole = true;

            // Enemy hit
            if (rayHit.collider.CompareTag("Enemy"))
            {
                EnemyAI enemy = rayHit.collider.GetComponent<EnemyAI>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            // TNT hit
            ExplosiveBox box = rayHit.collider.GetComponent<ExplosiveBox>();

            if (box != null)
            {
                box.Explode();
                spawnBulletHole = false;
            }

            // Only spawn decal if allowed
            if (spawnBulletHole)
            {
                GameObject hole = Instantiate(
                    bulletHoleGraphic,
                    rayHit.point,
                    Quaternion.LookRotation(rayHit.normal)
                );

                hole.transform.SetParent(rayHit.collider.transform);
            }
        }





        bulletsLeft--;
        bulletsShot--;


        Invoke("ResetShot", timeBetweenShooting);


        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }




}
