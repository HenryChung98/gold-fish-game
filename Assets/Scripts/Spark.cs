using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    private float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.2f);

   
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.direction == 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (GameManager.instance.direction == 1){
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }
        else if (GameManager.instance.direction == 2){
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (GameManager.instance.direction == 3){
            transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        }
        else if (GameManager.instance.direction == 4){
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (GameManager.instance.direction == 5){
            transform.rotation = Quaternion.Euler(0f, 0f, 225f);
        }
        else if (GameManager.instance.direction == 6){
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (GameManager.instance.direction == 7){
            transform.rotation = Quaternion.Euler(0f, 0f, 315f);
        }
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

    }
   
    }
