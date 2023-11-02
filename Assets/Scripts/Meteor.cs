using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
