using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletHolder;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    // Start is called before the first frame update
    Vector3 mousePosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 shootDirection = (mousePosition - transform.position).normalized;
            transform.up = shootDirection;
            Attack(shootDirection);
        }
    }

    private void LateUpdate()
    {

    }

    private void Attack(Vector2 direction)
    {
        bullet.transform.position = attackPoint.position;
        bullet.GetComponent<Bullet>().SetDirection(direction);
       
    }


}
