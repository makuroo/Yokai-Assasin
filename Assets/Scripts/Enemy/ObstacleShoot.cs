using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShoot : MonoBehaviour
{

  [SerializeField] private ObstacleProjectile projectile;
  [SerializeField] private Animator animator;
  private int damage;
  bool shoot = false;
  [SerializeField] private float fireRate;

  private const string ShootTrigger = "ShootTrigger";

  private void Awake()
  {
    // animator = GetComponent<Animator>();
  }

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
    yield return new WaitForSeconds(fireRate);
    Instantiate(projectile, transform.position, transform.rotation);
    yield return new WaitForSeconds(fireRate - 0.5f);

    animator.ResetTrigger(ShootTrigger);
    shoot = false;
  }


}
