using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x > 9.5f){
            Destroy(gameObject);
        }
    }
}
