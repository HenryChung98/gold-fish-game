using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    private float score;
    [SerializeField] 
    public float hp;
    [SerializeField] 
    private GameObject crystal;
    [SerializeField] 
    private float moveSpeed = 5f;
    private float verticalSpeed;
    private float minY = -3.0f;
    private float maxY = 3.0f;
    private float minX = -10f;
    private int createCrystal;

    private Color originalColor;
    private Color frozen = new Color(0, 255, 255, 200);
    private bool isMovementEnabled;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDirection;
    void Start()
    {
        isMovementEnabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        
        // 각 객체에 랜덤한 방향을 할당
        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-3.0f, 3.0f), 0).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementEnabled)
        {
        verticalSpeed = Random.Range(1.0f, 3.0f);
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Sin(Time.time) * verticalSpeed;
        transform.position += Vector3.up * verticalMovement * Time.deltaTime;
        }
        if (transform.position.x < minX){
            Destroy(gameObject);
        }
        // y 위치 제한
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Bullet")){
            hp -= GameManager.instance.damage / 2;
            spriteRenderer.color = Color.red;

            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp -= GameManager.instance.damage;
                Debug.Log("Critical!");
            }
            StartCoroutine(becomeOriginalColor());
            if (GameManager.instance.isPiercing == false){
            Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Spark")){
            hp -= GameManager.instance.damage;
            spriteRenderer.color = Color.red;
            
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp -= GameManager.instance.damage * 2;
                Debug.Log("Critical!");
            }
            StartCoroutine(becomeOriginalColor());
        }
        if (other.gameObject.CompareTag("Meteor")){
            hp -= GameManager.instance.damage / 3;
            spriteRenderer.color = Color.red;
            
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp -= (GameManager.instance.damage / 3) * 2;
                Debug.Log("Critical!");
            }
            StartCoroutine(becomeOriginalColor());

            
        }
        if (other.gameObject.CompareTag("ShadowBullet")){
            hp -= GameManager.instance.damage / 5f;
            spriteRenderer.color = Color.red;
            
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp -= (GameManager.instance.damage / 5) * 2;
                Debug.Log("Critical!");
            }
            StartCoroutine(becomeOriginalColor());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ice")){
                spriteRenderer.color = frozen;
                isMovementEnabled = false;
                StartCoroutine(unfrozen());
            }

            if (hp <= 0){
                Destroy(gameObject);
                // create crystal
                createCrystal = Random.Range(0, 5); 
                if (createCrystal == 3){
                    Instantiate(crystal, transform.position, Quaternion.identity);
                    }

                // get score
                for (int i = 0; i <= 3; i++){
                GameManager.instance.getScore(); 
                }

                // life steal
                if (GameManager.instance.isLifeSteal == true){
                    GameManager.instance.hp -= 5;
                    if (GameManager.instance.hp <= 0){
                        GameManager.instance.hp = 0;
                    }
                    GameManager.instance.updateHpBar();
                }
            }
        }
        private IEnumerator becomeOriginalColor(){
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = originalColor;
            }
        private IEnumerator unfrozen(){
        yield return new WaitForSeconds(2f);
        spriteRenderer.color = originalColor;
        isMovementEnabled = true;
    }
    }

