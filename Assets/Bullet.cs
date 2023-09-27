using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Bullet : MonoBehaviour
{
    private bool isShooting;
    [SerializeField] private float speed;
    private Vector2 direction;

    private Rigidbody2D rb2D;

    public bool IsShooting { get => isShooting; set => isShooting = value; }

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        //IsShooting = false;
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


    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        gameObject.SetActive(true);
        IsShooting = true;
    }

}
