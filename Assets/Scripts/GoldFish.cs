using UnityEngine;

public class BounceOffScreen : MonoBehaviour
{
    private float minX, minY, maxX, maxY;
    private Rigidbody2D rb;

    public float speed = 5f;
    public float rotationSpeed = 45f; // 회전 속도

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Camera mainCamera = Camera.main;

        // 화면의 경계 위치 계산
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y;

        // 초기 방향 설정
        Vector2 initialDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.velocity = initialDirection.normalized * speed;
    }

    void Update()
    {
        // 화면 경계에 닿았을 때 튕기도록 로직
        Vector2 position = transform.position;

        if (position.x < minX || position.x > maxX)
        {
            Vector2 newVelocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            rb.velocity = newVelocity;

            // 각속도를 변경하여 회전하도록 설정
            rb.angularVelocity = rotationSpeed;
        }

        if (position.y < minY || position.y > maxY)
        {
            Vector2 newVelocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            rb.velocity = newVelocity;

            // 각속도를 변경하여 회전하도록 설정
            rb.angularVelocity = rotationSpeed;
        }
    }
}