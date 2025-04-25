using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Configuración Básica")]
    [SerializeField] float moveSpeed;
    [SerializeField] float tiltAmount = 25f;

    [Header("Control por Giroscopio")]
    [SerializeField] float gyroSensitivity = 0.02f; // Más bajo = más sensible
    [SerializeField] bool invertGyro = true;
    [SerializeField] float deadZone = 0.1f; // Zona muerta para pequeños movimientos

    private Rigidbody rb;
    private Quaternion initialGyroRotation;
    private float verticalInput;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeGyro();
        SpawnSelectedShip();
        moveSpeed = GameManager.Instance.selectedShip.handling;
    }

    void InitializeGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            initialGyroRotation = Input.gyro.attitude;
        }
    }

    void Update()
    {
        GetInput();
        ApplyTilt();
    }

    void FixedUpdate()
    {
        MoveShip();
        ClampPosition();
    }

    void GetInput()
    {
        switch (GameManager.Instance.controlScheme)
        {
            case GameManager.ControlType.Accelerometer:
                HandleAccelerometer();
                break;

            case GameManager.ControlType.Gyroscope:
                HandleGyroInput();
                break;
        }
    }

    void HandleAccelerometer()
    {
        verticalInput = Input.acceleration.y;
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
    }

    void HandleGyroInput()
    {
        if (!SystemInfo.supportsGyroscope) return;

        // 1. Obtener rotación actual del dispositivo
        Quaternion currentGyro = Input.gyro.attitude;

        // 2. Calcular rotación relativa a la posición inicial
        Quaternion relativeRotation = Quaternion.Inverse(initialGyroRotation) * currentGyro;

        // 3. Convertir a ángulos de Euler y aplicar sensibilidad
        Vector3 euler = relativeRotation.eulerAngles;
        if (euler.z > 180f) euler.z -= 360f; // Convertir a rango -180 a 180

        // 4. Aplicar configuración de control
        verticalInput = invertGyro ? -euler.z : euler.z;
        verticalInput *= gyroSensitivity;

        // 5. Aplicar zona muerta y límites
        if (Mathf.Abs(verticalInput) < deadZone) verticalInput = 0f;
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
    }

    void MoveShip()
    {
        Vector3 movement = new Vector3(0f, verticalInput * moveSpeed, 0f);
        rb.linearVelocity = movement;
    }

    void ClampPosition()
    {
        float yPos = Mathf.Clamp(
            transform.position.y,
            -Camera.main.orthographicSize + 1f,
            Camera.main.orthographicSize - 1f
        );

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    void ApplyTilt()
    {
        if (GameManager.Instance.controlScheme == GameManager.ControlType.Gyroscope)
        {
            float tilt = verticalInput * tiltAmount;
            transform.rotation = Quaternion.Euler(0f, 0f, -tilt);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void SpawnSelectedShip()
    {
        if (GameManager.Instance.selectedShip != null)
        {
            Instantiate(
                GameManager.Instance.selectedShip.shipModel,
                transform.position,
                Quaternion.identity,
                transform
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            GameManager.Instance.TakeDamage();
            Destroy(other.gameObject);
        }
    }
}