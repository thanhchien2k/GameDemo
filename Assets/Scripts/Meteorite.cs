using DG.Tweening;
using Unity.Mathematics;
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
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        Vector3 screenPosition = new Vector3(Random.Range(0, screenWidth), Random.Range(screenHeight, screenHeight + 500f), 0);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newPos.x, newPos.y);

        speed = Random.Range(minspeed,maxspeed);
        CurrentHealth = (int)Random.Range(1, maxhealth);
        transform.localScale = Vector3.one *CurrentHealth;
        lifeTime = 0;
    }

    private void Start()
    {

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
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (gameObject.GetComponent<Meteorite>().CurrentHealth >= collision.gameObject.GetComponent<Meteorite>().currentHealth)
            {
                holder = GetComponentInParent<MeteoriteHolder>();
                Meteorite meteorite = holder.meteoriteHolder.GetObject().GetComponent<Meteorite>();
                Vector3 centerPosition = (transform.position + collision.transform.position) / 2f;
                meteorite.gameObject.SetActive(true);
                meteorite.speed = (speed + collision.GetComponent<Meteorite>().speed) / 2f;
                meteorite.CurrentHealth = Mathf.Clamp((CurrentHealth + collision.GetComponent<Meteorite>().CurrentHealth), 1, 2 * maxhealth);
                meteorite.SetMeteorite(centerPosition, meteorite.CurrentHealth, meteorite.speed);
                meteorite.ChangeScale();
                if(collision.gameObject.activeInHierarchy) collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

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

    private void ChangeScale()
    {
        Vector3 targetScale = new Vector3(currentHealth,currentHealth,transform.localScale.z);
        transform.DOScale(targetScale, 0.5f)
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
