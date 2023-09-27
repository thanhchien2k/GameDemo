using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControll : MonoBehaviour, IHealth
{
    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] private float maxhealth;
    public float MaxHealth { get=> maxhealth; set=> maxhealth=value; }
    [HideInInspector]public float CurrentHealth { get; set; }
    
    private bool isDie;
    public bool IsDie { get => isDie; set => isDie = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        isDie = false;
    }

    // Update is called once per frame
    void Update() 
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        
        if(CurrentHealth == 0)
        {
            IsDie = true;
            Die();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    private void Move()
    {

        rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage,0,MaxHealth);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
