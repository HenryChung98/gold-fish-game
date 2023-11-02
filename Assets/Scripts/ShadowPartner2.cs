using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPartnerSnd : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bullets;
    private int bulletIndex = 0;
    private float lastShotTime = 0f;
    private Rigidbody2D rb;
    [SerializeField]
    private Transform bulletShootTransform;

    // ShadowPartner의 이동 속도
    public float moveSpeed;

    private Transform player;

    private bool isColliding = false; // 충돌 상태 여부를 확인하기 위한 변수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = GameManager.instance.moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그를 가진 GameObject를 찾습니다.

        // Rigidbody2D 설정
        rb.gravityScale = 0; // 중력을 0으로 설정하여 중력의 영향을 받지 않도록 합니다.
        rb.angularDrag = 0; // Angular Drag를 0으로 설정하여 회전이 떨리지 않도록 합니다.
    }

    void Update()
    {
        if (GameManager.instance.shadowMove == true)
        {
            if (!isColliding) // 충돌 중이 아닐 때만 이동
            {
                // 플레이어와의 방향 벡터 계산
                Vector3 newPos = new Vector3(transform.position.x + 1f, transform.position.y - 0.7f, transform.position.z);
                Vector2 directionToPlayer = (player.position - newPos).normalized;

                // 이동 벡터 계산
                Vector2 moveDelta = directionToPlayer * moveSpeed * Time.deltaTime;

                // 움직인 후 위치 계산
                Vector2 newPosition = rb.position + moveDelta;
                rb.MovePosition(newPosition);
            }

            // 총 발사
            shoot();
        }
    }

    // 일정 간격으로 호출되어 총을 발사하는 메서드
    void shoot()
    {
        if (Time.time - lastShotTime > GameManager.instance.bulletShootInterval)
        {
            Instantiate(bullets[bulletIndex], bulletShootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

}