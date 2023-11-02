using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTentacleAttack : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    public float waitTime = 0.1f;
    private Vector3[] targetPositions;
    private int currentPositionIndex = 0;
    private float waitTimer = 0f;
    void Start()
    {
        float randomX = Random.Range(-5.5f, 1f);
        targetPositions = new Vector3[]
        {
            new Vector3(randomX, -11, 0),
            new Vector3(randomX, -8, 0),
            new Vector3(randomX, -11, 0),
            new Vector3(randomX, 0, 0),
            new Vector3(randomX, -11, 0)
        };

        // 초기 위치 설정
        transform.position = targetPositions[currentPositionIndex];
        Destroy(gameObject, 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= waitTime)
        {
            currentPositionIndex = (currentPositionIndex + 1) % targetPositions.Length;
            waitTimer = 0f;
        }

        // 목표 위치로 부드럽게 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentPositionIndex], moveSpeed * Time.deltaTime);

    }
}
