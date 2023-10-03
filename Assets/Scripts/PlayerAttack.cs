using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private BulletHolder bulletHolder;
    [SerializeField] private Transform attackPoint;
    // Start is called before the first frame update
    Vector3 mousePosition;
    private Vector2 shootDirection;

    public Vector2 ShootDirection { get => shootDirection; set => shootDirection = value; }

    void Start()
    {
        bulletHolder = FindObjectOfType<BulletHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3 (mousePosition.x, mousePosition.y, 0);
        shootDirection = (mousePosition - transform.position).normalized;
        transform.up = shootDirection;
        
        if (Input.GetMouseButtonDown(0))
        {
            bulletHolder.ShootBullet(attackPoint.position, mousePosition);
        }
    }

}
