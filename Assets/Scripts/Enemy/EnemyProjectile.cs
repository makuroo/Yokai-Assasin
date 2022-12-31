using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    private bool canBeParried = false;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = FindObjectOfType<Player>().transform.position;
        Destroy(gameObject, 2f);
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
            Debug.Log(canBeParried);
        }

        if (canBeParried && collision.CompareTag("Projectile"))
        {
            Debug.Log("hit");
            Destroy(gameObject);
            canBeParried = false;
        }
    }
}
