using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BikeController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;

    private Rigidbody2D rb;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            input = Vector2.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        input = new Vector2(h, v).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.isGameOver = true;
        }
    }
}
