using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpFood : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5f;
    private float minX = -10f;
    private float verticalSpeed;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-3.0f, 3.0f), 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        verticalSpeed = Random.Range(1.0f, 3.0f);
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Sin(Time.time) * verticalSpeed;
        transform.position += Vector3.up * verticalMovement * Time.deltaTime;
        
        if (transform.position.x < minX){
            Destroy(gameObject);
        }
    }
}
