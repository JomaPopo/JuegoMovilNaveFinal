using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform firePoint;

    [Header("Configuración")]
    private Ship currentShip;
    private bool isShooting;
    private float nextShotTime;

    void Start()
    {
        // Obtener datos del ScriptableObject
        currentShip = GameManager.Instance.selectedShip;
    }

    void Update()
    {
        HandleTouchInput();
        if (isShooting && Time.time >= nextShotTime) Shoot();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) isShooting = true;
            if (touch.phase == TouchPhase.Ended) isShooting = false;
        }
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetBullet();
        bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        nextShotTime = Time.time + currentShip.cadence;
    }
}