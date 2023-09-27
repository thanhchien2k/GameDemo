using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteHolder : MonoBehaviour
{
    [SerializeField] private GameObject meteoritePrefab;
    [SerializeField] private int minBullet;
    [SerializeField] private int maxBullet;
    private ObjectPool meteoriteHolder;


    private void Start()
    {
        // Khởi tạo Object Pool cho đạn, bạn có thể thay đổi initialSize và parentTransform tùy theo cần
        meteoriteHolder = new ObjectPool(meteoritePrefab, minBullet, maxBullet, transform);
        Invoke("SpwanMeteorite", 2f);

    }

    private void Update()
    {
        
    }
    public void ReturnBullet(GameObject bullet)
    {
        // Trả đạn về Object Pool (nếu cần)
        meteoriteHolder.ReturnObject(bullet);
    }

    private void SpwanMeteorite()
    {
        int numberOfMeterial = Random.Range(minBullet, maxBullet - 2*minBullet);
        for (int i = 1; i <= numberOfMeterial; i++)
        {
            Meteorite meteorite = meteoriteHolder.GetObject().GetComponent<Meteorite>();
            meteorite.gameObject.SetActive(true);
        }
    }
}
