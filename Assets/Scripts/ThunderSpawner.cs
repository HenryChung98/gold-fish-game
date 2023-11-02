using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpawner : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField] 
    private GameObject thunder;
    private Vector3 targetPosition; 

    void Start(){
        StartCoroutine(spawnThunder());
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        // 시작 위치와 끝 위치 사이의 랜덤한 위치 생성
        float randomX = Random.Range(0f, -8.5f);
        targetPosition = new Vector3(randomX, 4.8f, 0);
    }

    private IEnumerator spawnThunder(){
            while (true){
                Instantiate(thunder, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
                
            }
        }
}
