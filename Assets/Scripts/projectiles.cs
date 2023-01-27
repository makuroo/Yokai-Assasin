using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D projectileRb;
    private int damage = 3;
    private Player player;
    public  Shoot shoot;

    // Start is called before the first frame update
    void Start()
    {
        shoot = FindObjectOfType<Shoot>();
        Vector3 rotation = transform.position - shoot.mousePos;
        projectileRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot-90);
        projectileRb.velocity = new Vector3(shoot.direction.x,shoot.direction.y).normalized * speed;
        Destroy(gameObject, 2f);
        damage = player.damage;
        Debug.Log("mouse pos: "+shoot.mousePos);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
    }
}
