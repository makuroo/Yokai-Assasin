using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damageDuration = 3.0f;
    public int damageAmount = 10;
    private float timer = 0.0f;
    private bool playerInside = false;
    public Player player;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    public void ApplyDamage()
    {
        if(playerInside)
            player.TakeDamage(damageAmount);
    }
}