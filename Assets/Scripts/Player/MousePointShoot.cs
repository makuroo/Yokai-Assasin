using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointShoot : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public float rotz;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 2f;
        transform.rotation = Quaternion.Euler(0, 0, rotz);
        transform.localScale = transform.parent.localScale;

    }
}
