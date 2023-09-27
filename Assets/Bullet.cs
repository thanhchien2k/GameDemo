using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class Bullet : MonoBehaviour
{
    private bool isShooting;
    private BulletHolder bulletHolder;
    [SerializeField] private float speed;
    private Vector2 direction;
    float lifeTime;


    public bool IsShooting { get => isShooting; set => isShooting = value; }

    private void Awake()
    {
        bulletHolder = GetComponentInParent<BulletHolder>();
        gameObject.SetActive(false);
    } 

    // Update is called once per frame
    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        if (!IsShooting) return;
        
        transform.Translate(direction * speed * Time.deltaTime);
        lifeTime += Time.deltaTime;
        if (lifeTime > 3f)
        {
            DesActive();
        }


    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        IsShooting = true;
        gameObject.SetActive(true);
        lifeTime = 0;
        
    }

    public void DesActive()
    {
        IsShooting = false;
        gameObject.SetActive(false);
        
    }

}
