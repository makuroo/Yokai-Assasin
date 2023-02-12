using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private Player player;
    private float cooldown = 0f;
    public Vector3 mousePos;
    public Camera mainCam;
    public Vector3 direction;
    public Vector2 tempMousePos;
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private RuntimeAnimatorController[] animController;
    private int specialCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        anim = projectile.GetComponent<Animator>();
        sr = player.GetComponent<SpriteRenderer>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player.OnPickUpPowerUps += Player_OnPickUpPowerUps;
        player.OnLeftMouseClick += Player_OnLeftMouseClick;
    }

    private void Player_OnLeftMouseClick(object sender, System.EventArgs e)
    {
        InstantiateProjectile();
    }

    private void Player_OnPickUpPowerUps(object sender, System.EventArgs e)
    {
        specialCount = 3;
        ChangeThrowable();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;

        cooldown -= Time.deltaTime;
        if (specialCount <= 0)
        {
            player.damage = 2;
            specialCount = 0;
            sr.sprite = sprite[0];
            anim.runtimeAnimatorController = animController[0];
        }
    }

    private void ChangeThrowable()
    {
        int spriteIndex = 0;
        if (spriteIndex == 0)
            sr.sprite = sprite[spriteIndex++];
        anim.runtimeAnimatorController = animController[1];
    }

    private void InstantiateProjectile()
    {
        if ((Mathf.Abs(direction.x) > 0.1 || Mathf.Abs(direction.y) > 0.1))
        {
            if (cooldown <= 0)
            {
                player.shoot = false;
                tempMousePos = mousePos;
                cooldown = 0.5f;
                Instantiate(projectile, transform.position, transform.rotation);
                specialCount--;
            }
        }
    }
}
