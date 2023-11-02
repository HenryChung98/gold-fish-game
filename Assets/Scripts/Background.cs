using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Color originalColor;
    public Color targetColor = Color.red; // 목표 색상
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

 
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < -47){
            transform.position += new Vector3(75f, 0, 0);

        }
        
        
    }

    // private void OnTriggerEnter2D(Collider2D other){
    //     if (other.gameObject.CompareTag("Boss")){
    //         EnemySpawner bossAppear = gameObject.GetComponent<EnemySpawner>();
    //         if (bossAppear.spawnCount >= 5 && bossAppear.bossSlayed == false){
    //             spriteRenderer.color = targetColor;
    //         }

    //         else if (bossAppear.spawnCount >= 5 && bossAppear.bossSlayed == true){
    //             spriteRenderer.color = originalColor;
    //         }
    //     }
    // }
    // IEnumerator changeBackgroundColor(Color start, Color target)
    // {
    //     float t = 0f;

    //     while (t < 1f)
    //     {
    //         t += Time.deltaTime * 2f;
    //         originalColor = Color.Lerp(start, target, t);
    //         yield return null;
    //     }

    // }

}
