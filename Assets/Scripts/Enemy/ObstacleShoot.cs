using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShoot : MonoBehaviour
{
    [SerializeField] private ObstacleProjectile projectile;
    private int damage;
    bool shoot = false;
    [SerializeField]private float fireRate;

    private void Update()
    {
        if (shoot == false)
        {
            StartCoroutine(Shoot(fireRate));
        }
    }

    private IEnumerator Shoot(float fireRate)
    {
        shoot = true;
        yield return new WaitForSeconds(fireRate);
        Instantiate(projectile, transform.position, transform.rotation);
        shoot = false;
    } 

}
