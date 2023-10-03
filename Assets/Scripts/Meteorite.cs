using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorite : MonoBehaviour, IHealth
{
    private float speed;
    private float speedx;
    [Range(1.5f, 5f)]
    [SerializeField]private float maxspeed;
    [Range(1.5f, 5f)]
    [SerializeField]private float minspeed;
    [SerializeField] private GameObject player;
    [SerializeField] private float maxhealth;
    [SerializeField] private float currentHealth;
    [SerializeField]private MeteoriteHolder holder;
    public float MaxHealth { get => maxhealth; set => maxhealth = value; }
    [HideInInspector] public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private bool isDie;
    public bool IsDie { get => isDie; set => isDie = value; }

    float screenWidth = Screen.width;
    float screenHeight = Screen.height;
    float lifeTime;
    

    private void Awake()
    {
        isDie = false;
    }

    private void OnEnable()
    {
        CurrentHealth = (int)Random.Range(1, maxhealth);
        Vector3 screenPosition = new Vector3(Random.Range(90f*currentHealth, screenWidth-90f*currentHealth), Random.Range(screenHeight, screenHeight + 500f), 0);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, newPos.y);
        speed = Random.Range(minspeed,maxspeed);
        speedx = Random.Range(0,minspeed-2);
        if (newPos.x > player.transform.position.x)
        {
            speedx =-speedx;
        }
        //ChangeSpeedX(newPos);
        transform.localScale = Vector3.one *CurrentHealth;
        lifeTime = 0;
    }

    private void Start()
    {
        holder = GetComponentInParent<MeteoriteHolder>();
    }
    void Update()
    {
        if (PlayerControll.isDie) return;
        MoveDown();
        lifeTime += Time.deltaTime;
        if (lifeTime > 5f)
        {
            DesActive();
        }

        if (CurrentHealth == 0)
        {
            IsDie = true;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            collision.gameObject.GetComponent<Bullet>().DesActive();
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (gameObject.GetComponent<Meteorite>().CurrentHealth >= collision.gameObject.GetComponent<Meteorite>().currentHealth)
            {
                speed = (speed + collision.GetComponent<Meteorite>().speed) / 2f;
                currentHealth = Mathf.Clamp((CurrentHealth + collision.GetComponent<Meteorite>().CurrentHealth), 1, 2 * (maxhealth-1)-1);
                if(collision.gameObject.activeInHierarchy) collision.gameObject.GetComponent<Meteorite>().DesActive();
            }
        }

        ChangeScale();
    }


    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, MaxHealth);
    }

    public void Die()
    {
        DesActive();
    }

    private void MoveDown()
    {
        transform.Translate(new Vector3(speedx,-speed,0)*Time.deltaTime);
    }

    private void DesActive()
    {
        holder.ReturnToPool(gameObject);
    }

    private void ChangeScale()
    {
        Vector3 targetScale = new Vector3(currentHealth,currentHealth,transform.localScale.z);
        transform.DOScale(targetScale, 0.8f)
            .SetEase(Ease.OutBounce);
    }

    public void SetMeteorite(Vector3 pos, float health, float speed)
    {
        transform.position = new Vector3(pos.x, pos.y);
        this.speed = speed;
        CurrentHealth = health;
        lifeTime = 0;
    }
}
