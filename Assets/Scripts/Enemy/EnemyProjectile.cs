using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    private Rigidbody2D rb;
    Player player;
    private bool canBeParried = false;
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
        if (collision.gameObject.CompareTag("ParryField"))
        {
            canBeParried = true;
        }

        if (canBeParried && collision.CompareTag("Projectile") && player.currStamina >= player.parryStamina)
        {
            player.parried = true;
            player.VerifyParryStaminaUsage();
            canBeParried = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player")){
            collision.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
