using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [Header("Configuración")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initialPoolSize = 20;

    private Queue<GameObject> bullets = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count == 0) ExpandPool();
        GameObject bullet = bullets.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }

    void ExpandPool()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}