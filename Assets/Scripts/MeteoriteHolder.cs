using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteHolder : MonoBehaviour
{
    [SerializeField] private GameObject meteoritePrefab;
    [SerializeField] private int minBullet;
    [SerializeField] private int maxBullet;
    [HideInInspector]public ObjectPool meteoriteHolder;


    private void Start()
    {
        // Khởi tạo Object Pool cho đạn, bạn có thể thay đổi initialSize và parentTransform tùy theo cần
        meteoriteHolder = new ObjectPool(meteoritePrefab, minBullet, maxBullet, transform);
        InvokeRepeating("SpwanMeteorite", 0.5f, 1f);

    }
    public void ReturnBullet(GameObject bullet)
    {
        // Trả đạn về Object Pool (nếu cần)
        meteoriteHolder.ReturnObject(bullet);
    }

    private void SpwanMeteorite()
    {
        if (PlayerControll.isDie) return;
        int numberOfMeterial = Random.Range(1, 2);
        for (int i = 1; i <= numberOfMeterial; i++)
        {
            Meteorite meteorite = meteoriteHolder.GetObject().GetComponent<Meteorite>();
            meteorite.gameObject.SetActive(true);
        }
    }

    internal object GetObject()
    {
        throw new System.NotImplementedException();
    }
}
