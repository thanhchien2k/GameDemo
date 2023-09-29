using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerControll : MonoBehaviour, IHealth
{
    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private PlayerAttack attack;
    Vector3 playerPos;

    [SerializeField] private float maxhealth;
    public float MaxHealth { get=> maxhealth; set=> maxhealth=value; }
    [HideInInspector]public float CurrentHealth { get; set; }
    
    public static bool isDie ;
    public bool IsDie { get => isDie; set => isDie = value; }

    private void Awake()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z) ;
        rb = GetComponent<Rigidbody2D>();
        attack =GetComponent<PlayerAttack>();
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
        
        if(CurrentHealth == 0 && gameObject.activeInHierarchy)
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

        rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed) ;
        
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage,0,MaxHealth);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
       CurrentHealth = CurrentHealth = Mathf.Clamp(1, 0, MaxHealth);
        isDie = false;
        transform.position = playerPos;
        gameObject.SetActive(true);
    }

}
