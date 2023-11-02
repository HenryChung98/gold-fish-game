using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5f;
    private float minX = -10f;
    private Rigidbody2D rigidbody; // Rigidbody2D 변수를 선언

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D를 가져와 변수에 할당
        Jump();
    }

    void Jump()
    {
        float randomJumpForce = Random.Range(-1f, 1f); 
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2.0f, -1.0f); 
        rigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
        if (rigidbody.velocity.y <= -1.5f || rigidbody.velocity.y >= 1.5f)
        {
            // 일정 높이에 도달하면 Rigidbody2D를 멈춥니다.
            rigidbody.velocity = Vector2.zero;
        }
    }
}