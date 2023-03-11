using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    Player player;
    private Vector3 targetPos;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = FindObjectOfType<Player>().transform.position;
        player = FindObjectOfType<Player>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Projectile") && player.currStamina >= player.parryStamina)
        {
            player.parried = true;
            player.VerifyParryStaminaUsage();
            collision.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Parry");
            collision.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject, .1f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
