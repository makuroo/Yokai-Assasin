using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Action : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private float nextFire = 0;
    private bool inRange = false;
    [SerializeField] Transform target;
    private Vector3 posDiff;
    private float fixedZ = -90;
    private float rotz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
        if (inRange)
        {
            posDiff = target.position - transform.position;
            rotz = Mathf.Atan2(posDiff.y, posDiff.x) * Mathf.Rad2Deg + fixedZ;
            transform.rotation = Quaternion.Euler(0, 0, rotz);
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inRange)
        {
            if (nextFire <= 0)
            {
                Shoot();
                nextFire = 2f;
            }
        }
        nextFire -= Time.deltaTime;
    }

    private void Shoot()
    {
       StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
         Instantiate(projectile, transform.position, transform.rotation);
         yield return new WaitForSeconds(2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inRange = false;
    }
}
