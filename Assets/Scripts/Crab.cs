using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crab : MonoBehaviour
{
    [SerializeField] 
    private Slider crabHPBar;
    private Vector3 startPosition = new Vector3(0, -4, 0); // 시작 위치
    private Vector3 endPosition = new Vector3(7.5f, 4, 0); // 끝 위치
    private Vector3 targetPosition; // 이동할 위치
    [SerializeField] 
    private GameObject attackOne;


    //-----------common objects-----------//
    public float hp = 1f;
    [SerializeField] 
    private float moveSpeed = 5f;
    [SerializeField] 
    private GameObject crystal;
    private Color originalColor;
    private Color frozen = new Color(0, 255, 255, 200);
    private SpriteRenderer spriteRenderer;
    private bool isMovementEnabled;
    [SerializeField] 
    private GameObject thunderCloud;

    void Start()
    {
        //-----------common objects-----------//
        isMovementEnabled = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Vector3 cloudPos = new Vector3(0f, 7f, 0f);
        Instantiate(thunderCloud, cloudPos, Quaternion.identity);
        SetRandomTargetPosition();
        StartCoroutine(attack1());
    }


    void Update()
    {
        if (isMovementEnabled)
        {
        
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 목표 위치에 도달하면 다시 새로운 랜덤 위치로 설정
        if (transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
        }
    }

    void SetRandomTargetPosition()
    {
        // 시작 위치와 끝 위치 사이의 랜덤한 위치 생성
        float randomX = Random.Range(startPosition.x, endPosition.x);
        float randomY = Random.Range(startPosition.y, endPosition.y);
        targetPosition = new Vector3(randomX, randomY, 0);
    }

    private IEnumerator attack1(){
            while (true){
                Instantiate(attackOne, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                
            }
        }





    //-----------common objects-----------//
    private void displayBar(){
        spriteRenderer.color = Color.red;
        RectTransform hpBarSize = crabHPBar.GetComponent<RectTransform>();
        float forBar = hp * 3f;
        hpBarSize.offsetMax = new Vector2(-forBar, hpBarSize.offsetMax.y);
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Bullet")){
            hp += GameManager.instance.damage;
            displayBar();
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp += GameManager.instance.damage * 2;
                Debug.Log("Critical!");
                displayBar();
            }
            StartCoroutine(becomeOriginalColor());
            Destroy(other.gameObject);
            }

        if (other.gameObject.CompareTag("Spark")){
            hp += GameManager.instance.damage / 2;
            displayBar();
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp += GameManager.instance.damage;
                Debug.Log("Critical!");
                displayBar();
            }
            StartCoroutine(becomeOriginalColor());
        }
        if (other.gameObject.CompareTag("Meteor")){
            hp += GameManager.instance.damage / 3;
            displayBar();
            
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp += (GameManager.instance.damage / 3) * 2;
                Debug.Log("Critical!");
                displayBar();
            }
            StartCoroutine(becomeOriginalColor());
        }
        if (other.gameObject.CompareTag("ShadowBullet")){
            hp += GameManager.instance.damage / 5;
            displayBar();
            
            // critical hit
            int randomNum = Random.Range(1, 101);
            if (randomNum <= GameManager.instance.criticalPercent){
                hp += (GameManager.instance.damage / 5) * 2;
                Debug.Log("Critical!");
                displayBar();
            }
            StartCoroutine(becomeOriginalColor());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ice")){
                spriteRenderer.color = frozen;
                isMovementEnabled = false;
                StartCoroutine(unfrozen());
            }

            if (hp >= 400f){ // here is boss max hp
                Destroy(gameObject);
                moveSpeed = 1f;
                EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();

                Vector3 newPos1 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                Instantiate(crystal, newPos1, Quaternion.identity);
                Vector3 newPos2 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                Instantiate(crystal, newPos2, Quaternion.identity);
                Vector3 newPos3 = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(crystal, newPos3, Quaternion.identity);
                Vector3 newPos4 = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
                Instantiate(crystal, newPos4, Quaternion.identity);
                Vector3 newPos5 = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
                Instantiate(crystal, newPos5, Quaternion.identity);
                
                // get score
                for (int i = 0; i <= 10; i++){
                    GameManager.instance.getScore();
                }

                // life steal
                if (GameManager.instance.isLifeSteal == true){
                    GameManager.instance.hp -= 5;
                    GameManager.instance.GetDamage();
                }
                }
        }

        private IEnumerator becomeOriginalColor(){
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = originalColor;
            }
    private IEnumerator unfrozen(){
        yield return new WaitForSeconds(1f);  // only crab 1 sec
        spriteRenderer.color = originalColor;
        isMovementEnabled = true;
    }
}
