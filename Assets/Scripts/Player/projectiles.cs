using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
  [SerializeField] private float speed = 10f;
  private Rigidbody2D projectileRb;
  private int damage;

  private Player player;
  public Shoot shoot;

  private Animator anim;

  // Start is called before the first frame update
  void Start()
  {
    GetReferences();
    Move();
    Destroy(gameObject, 1f);
  }

  private void Update()
  {
    if (player.parried)
    {
      anim.SetTrigger("Parry");
    }
  }

  private void Move()
  {
    Vector3 rotation = transform.position - shoot.mousePos;
    float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    projectileRb.velocity = new Vector3(shoot.direction.x, shoot.direction.y).normalized * speed;
  }

  private void GetReferences()
  {
    shoot = FindObjectOfType<Shoot>();
    projectileRb = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    damage = player.damage;
    anim = GetComponentInChildren<Animator>();
  }

    private void GetReferences()
    {
        shoot = FindObjectOfType<Shoot>();
        projectileRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        damage = player.damage;
        anim = GetComponentInChildren<Animator>();
    }
}
