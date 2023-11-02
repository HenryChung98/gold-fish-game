using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


//----------for skills----------//
    [SerializeField] 
    private GameObject shadow;
    [SerializeField] 
    private GameObject shadowSnd;

    [SerializeField]
    private GameObject[] bullets;
    private int bulletIndex = 0;

    [SerializeField]
    private Transform bulletShootTransform;
    private float shootLastShotTime = 0f;

    [SerializeField]
    private GameObject[] sparks;
    private int sparkIndex = 0;

    [SerializeField]
    private Transform sparkShootTransform;
    private float sparkLastShotTime = 0f;
    private Color frozen = new Color(0, 255, 255, 1f);
    private Color hide = new Color(255, 255, 255, 0.39f);

//----------for skills----------//

//----------Player----------//
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    public int expNum = 10;
    private bool isInvincible; // 무적 상태 여부
    private Color originalColor; // 원래의 색상
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
    public bool isMovementEnabled; // 이동 가능 여부 플래그

//----------Player----------//

//----------반동----------//
    private Rigidbody2D rb; // Rigidbody2D 추가
    private Vector3 impactDirection; // 밀려나는 방향 저장 변수

    void Start()
    {
        isMovementEnabled = true;
        isInvincible = false;
        mainCamera = Camera.main;

        // 화면 경계 계산
        Vector3 minBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = minBound.x;
        maxX = maxBound.x;
        minY = minBound.y;
        maxY = maxBound.y;

        // 스프라이트 렌더러 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 원래 색상 저장
        originalColor = spriteRenderer.color;
        
        rb = GetComponent<Rigidbody2D>();
        
        StartCoroutine(invisible());
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementEnabled)
        {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);

        // 이동 벡터에 이동 속도를 곱한 후, Time.deltaTime을 곱하여 시간에 따른 움직임을 계산
        Vector3 moveDelta = moveDirection.normalized * GameManager.instance.moveSpeed * Time.deltaTime;

        // 움직인 후 위치 계산
        Vector3 newPosition = transform.position + moveDelta;

        // 위치를 화면 경계 내로 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // 실제로 이동
        transform.position = newPosition;
        }
        


    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!isInvincible)
        {
//---------------------Enemy-----------------------------//
            if (other.gameObject.CompareTag("Enemy"))
            {
                isInvincible = true;
                spriteRenderer.color = Color.red;
                Vector3 enemyPosition = other.transform.position;
                Vector3 moveBackDirection = (transform.position - enemyPosition).normalized;
                rb.AddForce(moveBackDirection * 5f, ForceMode2D.Impulse);   
                // 1초 후에 무적 상태 해제
                StartCoroutine(EndInvincibility());

                GameManager.instance.GetDamage();
                if (GameManager.instance.hp >= 450f){
                    Destroy(gameObject);
                    }
            }
            if (other.gameObject.CompareTag("Boss")){
                isInvincible = true;
                spriteRenderer.color = Color.red;
                Vector3 enemyPosition = other.transform.position;
                Vector3 moveBackDirection = (transform.position - enemyPosition).normalized;
                rb.AddForce(moveBackDirection * 30f, ForceMode2D.Impulse);
                StartCoroutine(EndInvincibilityBoss());


                GameManager.instance.GetDamage();
                if (GameManager.instance.hp >= 450f){
                    Destroy(gameObject);
                    //Destroy(shadow);
                    }
            }
            if (other.gameObject.CompareTag("Attack")){
                isInvincible = true;
                spriteRenderer.color = Color.red;
                Vector3 enemyPosition = other.transform.position;
                Vector3 moveBackDirection = (transform.position - enemyPosition).normalized;
                rb.AddForce(moveBackDirection * 5f, ForceMode2D.Impulse);   
                // 1초 후에 무적 상태 해제
                StartCoroutine(EndInvincibility());
                GameManager.instance.GetDamage();
                }
            if (other.gameObject.CompareTag("debuff")){
                spriteRenderer.color = frozen;
                isMovementEnabled = false;
                GameManager.instance.shadowMove = false; 
                StartCoroutine(becomeOriginal());
            }
            if (other.gameObject.CompareTag("StealAttack")){
                Turtle turtle = GameObject.FindGameObjectWithTag("Boss").GetComponent<Turtle>();
                
                turtle.hp -= 100f;
                turtle.displayBar();
                isInvincible = true;
                spriteRenderer.color = Color.red;
                Vector3 enemyPosition = other.transform.position;
                Vector3 moveBackDirection = (transform.position - enemyPosition).normalized;
                rb.AddForce(moveBackDirection * 5f, ForceMode2D.Impulse);   
                StartCoroutine(EndInvincibility());
                
                GameManager.instance.GetDamage();
            }
        }
//---------------------Enemy-----------------------------//

//---------------------Iteams--------------------//

            if (other.gameObject.CompareTag("Food"))
            {
                Destroy(other.gameObject);
                for (int i = 0; i <= expNum * 10; i++){
                GameManager.instance.GetEXP();
                }
                GameManager.instance.getScore();

            }
            if (other.gameObject.CompareTag("Crystal")){
                Destroy(other.gameObject);
                for (int i = 0; i <= expNum * 100; i++){
                    GameManager.instance.GetEXP();
                }
                for (int i = 0; i <= 2; i++){
                GameManager.instance.getScore();
                }
            }
            if (other.gameObject.CompareTag("HpFood")){
                Destroy(other.gameObject);
                GameManager.instance.hp = -GameManager.instance.armor;
                GameManager.instance.GetDamage();
                
            }
        
    }
//---------------------Iteams--------------------//
    private IEnumerator EndInvincibility()
    {
        isMovementEnabled = false;
        GameManager.instance.shadowMove = false; 
        yield return new WaitForSeconds(0.5f);
        rb.drag = 10.0f;
        // 무적 상태 해제
        isInvincible = false;
        // 원래 색상으로 복원
        spriteRenderer.color = originalColor;
        isMovementEnabled = true; 
        GameManager.instance.shadowMove = true; 
    }

    private IEnumerator EndInvincibilityBoss()
    {
        isMovementEnabled = false; 
        GameManager.instance.shadowMove = false; 
        yield return new WaitForSeconds(1f);
        rb.drag = 10.0f;
        // 무적 상태 해제
        isInvincible = false;
        // 원래 색상으로 복원
        spriteRenderer.color = originalColor;
        isMovementEnabled = true; 
        GameManager.instance.shadowMove = false; 
    }
    private IEnumerator becomeOriginal(){
        yield return new WaitForSeconds(3f);
        spriteRenderer.color = originalColor;
        isMovementEnabled = true;
        GameManager.instance.shadowMove = true; 
    }



    //--------------skills-----------------//
    public void shoot(){
        if (Time.time - shootLastShotTime > GameManager.instance.bulletShootInterval){
            Instantiate(bullets[bulletIndex], bulletShootTransform.position, Quaternion.identity);
            if (GameManager.instance.doubleShooting == true){
                Vector3 bulletNewPos = new Vector3(bulletShootTransform.position.x - 0.1f, bulletShootTransform.position.y, bulletShootTransform.position.z);
                Instantiate(bullets[bulletIndex], bulletNewPos, Quaternion.identity);
            }
            shootLastShotTime = Time.time;    
        }
    }
    private IEnumerator wait(){
        yield return new WaitForSeconds(0.1f);
        }

    public void sparking(){
        if (Time.time - sparkLastShotTime > GameManager.instance.sparkShootInterval){
            Spark spark = gameObject.GetComponent<Spark>();

            if (GameManager.instance.direction == 0)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x + 0.5f, sparkShootTransform.position.y - 2f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 1)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x + 2f, sparkShootTransform.position.y - 1f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 2)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x + 2f, sparkShootTransform.position.y + 0.5f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 3)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x + 1f, sparkShootTransform.position.y + 2f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 4)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x - 0.5f, sparkShootTransform.position.y + 2f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 5)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x - 2f, sparkShootTransform.position.y + 1f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 6)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x - 2f, sparkShootTransform.position.y - 0.5f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }
        else if (GameManager.instance.direction == 7)
        {
            Vector3 newPos = new Vector3(sparkShootTransform.position.x - 1.5f, sparkShootTransform.position.y - 2f, 0f);
            Instantiate(sparks[sparkIndex], newPos, Quaternion.identity);
        }


            sparkLastShotTime = Time.time;    
        }
    }


    private IEnumerator invisible(){
        while(true){
            if (GameManager.instance.isInvisible == true){
                isInvincible = true;
                spriteRenderer.color = hide;
                yield return new WaitForSeconds(3f);
                isInvincible = false;
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(20f);
            }
            else{
                    yield return null;
                }
            
        }
    }

    public void instantiateShadows(){
        Vector3 spawn1 = new Vector3(transform.position.x - 1f, transform.position.y + 0.7f, transform.position.z);
        Vector3 spawn2 = new Vector3(transform.position.x - 1f, transform.position.y - 0.7f, transform.position.z);
        Instantiate(shadow, spawn1, Quaternion.identity);
        Instantiate(shadowSnd, spawn2, Quaternion.identity);
    }
    
}