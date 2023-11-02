using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range;

    [SerializeField]
    private float moveSpeed = 10f;


    void Start()
    {
        range = 0.5f;//?
        Destroy(gameObject, range);//?
        
    }

    void Update()
    {
        range = 0.5f;//?
        Destroy(gameObject, range);//?
        if (GameManager.instance.isSizeup == true)
        {
            Vector3 size = new Vector3(2f, 2f, 1f);
            transform.localScale = size;
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (transform.position.x > 9.5f){
            Destroy(gameObject);
        }
    }

    public void upRange()
    {
        range += 0.2f;
    }
}