using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShoot : MonoBehaviour
{
    [SerializeField] private ObstacleProjectile projectile;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] shootPoint;
    private int damage;
    bool shoot = false;
    [SerializeField] private float fireRate;

    private const string ShootTrigger = "ShootTrigger";

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
        animator.SetTrigger(ShootTrigger);

        yield return new WaitForSeconds(1f);

        // InstantiateProjectile();

        yield return new WaitForSeconds(fireRate);

        animator.ResetTrigger(ShootTrigger);
        shoot = false;
    }

    public void InstantiateProjectile()
    {
        Instantiate(projectile, shootPoint[0].transform.position, shootPoint[0].rotation);
        Instantiate(projectile, shootPoint[1].transform.position, shootPoint[1].rotation);
    }

}
