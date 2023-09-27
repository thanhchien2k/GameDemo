using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorite : MonoBehaviour, IHealth
{
    private float speed;
    [Range(1.5f, 5f)]
    [SerializeField]private float maxspeed;
    [Range(1.5f, 5f)]
    [SerializeField]private float minspeed;

    [SerializeField] private float maxhealth;
    public float MaxHealth { get => maxhealth; set => maxhealth = value; }
    [HideInInspector] public float CurrentHealth { get; set; }

    private bool isDie;
    public bool IsDie { get => isDie; set => isDie = value; }

    float screenWidth = Screen.width;
    float screenHeight = Screen.height;
    float lifeTime;

    private void Awake()
    {
        isDie = false;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        speed = Random.Range(minspeed,maxspeed);
        CurrentHealth = (int)Random.Range(1, maxhealth);
        transform.localScale = Vector3.one *CurrentHealth;
        lifeTime = 0;
    }

    private void Start()
    {
        Vector3 screenPosition = new Vector3(Random.Range(0,screenWidth), Random.Range(screenHeight, screenHeight+100f), 0);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
    void Update()
    {
        MoveDown();
        lifeTime += Time.deltaTime;
        if (lifeTime > 5f)
        {
            DesActive();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            collision.gameObject.GetComponent<Bullet>().IsShooting = false;
            collision.gameObject.GetComponent<Bullet>().DesActive();
        }
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, MaxHealth);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void DesActive()
    {
        gameObject.SetActive(false);
    }

}
