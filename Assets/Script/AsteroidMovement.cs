using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Configuraci�n")]
    [SerializeField] float speed = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector2.left * speed;

    }

    private void FixedUpdate()
    {
       
    }
  
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destr")
        {
            Destroy(this.gameObject);
        }
    }

}