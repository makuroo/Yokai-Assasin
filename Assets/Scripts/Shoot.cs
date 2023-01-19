using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    private float cooldown = 0f;
    public Vector3 mousePos;
    public Camera mainCam;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        if (Input.GetMouseButtonDown(0) && (Mathf.Abs(direction.x) > 0.5 || Mathf.Abs(direction.y)>0.5))
        {
            if (cooldown <= 0)
            {
                cooldown = 0.5f;
                Instantiate(projectile, transform.position, transform.rotation);
            }
        }
        cooldown -= Time.deltaTime;
    }
}
