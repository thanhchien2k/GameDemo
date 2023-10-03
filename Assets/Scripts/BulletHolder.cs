using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int minBullet;
    [SerializeField]private int maxBullet;
    private ObjectPool bulletPool;


    private void Start()
    {
        // Khởi tạo Object Pool cho đạn, bạn có thể thay đổi initialSize và parentTransform tùy theo cần
        bulletPool = new ObjectPool(bulletPrefab, minBullet, maxBullet, transform);
    }

    public void ShootBullet(Vector2 position, Vector2 direction)
    {
        // Lấy một đối tượng đạn từ Object Pool
        Bullet bullet = bulletPool.GetObject().GetComponent<Bullet>();

        // Đặt vị trí và hướng cho đạn
        bullet.transform.position = position;
        bullet.SetDirection(direction);
    }

    public void ReturnBullet(GameObject bullet)
    {
        bulletPool.ReturnObject(bullet);
    }
}
