using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roll : MonoBehaviour
{
    public Player player;
    [SerializeField] int damageAmount = 2;
    [SerializeField]float speed = 2.0f;
    float damageInterval = 0.6f;
    [SerializeField] float resetTime = 5.0f;
    float lastDamageTime;
    Vector3 startPos;
    float lastResetTime;
    void Start()
    {
        player = FindObjectOfType<Player>();
        lastDamageTime = -damageInterval;

        startPos = transform.position;
        lastResetTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.position += movement;

        if (Time.time - lastResetTime >= resetTime)
        {
            ResetPosition();
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                player.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }

    void ResetPosition()
    {
        transform.position = startPos;
        lastResetTime = Time.time;
    }
}
