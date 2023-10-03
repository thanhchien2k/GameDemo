using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private bool isShooting;
    private BulletHolder bulletHolder;
    [SerializeField] private float speed;
    private Vector2 direction;
    float lifeTime;
    private Rigidbody2D rb;


    public bool IsShooting { get => isShooting; set => isShooting = value; }

    private void Awake()
    {
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        bulletHolder = GetComponentInParent<BulletHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        if (!IsShooting) return;
        
        Vector2 finalSpeed = new Vector2 (direction.x*speed, (direction.y)*speed);
        rb.velocity = finalSpeed;
        lifeTime += Time.deltaTime;
        if (lifeTime > 3f)
        {
            DesActive();
        }
    }

    public void SetDirection(Vector3 dir)
    {   
        direction = (dir - transform.position).normalized;
        IsShooting = true;
        gameObject.SetActive(true);
        lifeTime = 0;
    }

    public void DesActive()
    {
        IsShooting = false;
        bulletHolder.ReturnBullet(this.gameObject);
    }

}
