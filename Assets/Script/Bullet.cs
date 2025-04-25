using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] float bulletSpeed = 15f;
    [SerializeField] float lifeTime = 1.5f;
    public static event Action adpoints;
    void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    void Deactivate()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke("Deactivate");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            adpoints?.Invoke();
            BulletPool.Instance.ReturnBullet(gameObject);
           
        }
    }
}