using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWideAreaStone : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
