using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    //-----------bools-------------//
    private bool paused = false;
    private bool done = false;
    private bool checkSpark;
    public bool isPiercing;
    public bool doubleShooting;
    public bool isLifeSteal;
    public bool isInvisible;
    public bool isMeteor;
    public bool isSizeup;
    public bool isIce;

    //-----------bools-------------//


    //-----------about skills-------------//
    // spark
    public float sparkShootInterval = 1f;
    // attack speed
    public float bulletShootInterval;
    // move speed
    public float moveSpeed = 10f;
    // incrase damage
    public float damage = 1f;
    // get HP
    public float armor = 50f;
    // shadow partner
    [SerializeField] 
    private GameObject shadowPartner;

    // range
    int range = 0;
    // critical percent
    [SerializeField]
    public int criticalPercent = 0;

    // shadow partner
    public bool shadowMove; 
    //-----------about skills-------------//

    //-----------Status-------------//
    public float exp = 450f;
    private float addExp = 1f;    
    public float hp = 0f;
    private int level = 0;
    public int score = 0;
    [SerializeField] 
    private TextMeshProUGUI text;
    //-----------Status-------------//
    
    //-----------Objects-------------//
    public static GameManager instance = null;
    public int direction;
//-----------Buttons-------------//
    [SerializeField] 
    private GameObject pausePanel;
    [SerializeField] 
    private GameObject abilityPanel;
    private GameObject[] abilityButtons1;     

    private GameObject[] abilityButtons2; 

    private GameObject[] abilityButtons3; 
    

    [SerializeField] 
    private GameObject asButton;
    [SerializeField] 
    private GameObject sizeupButton;
    [SerializeField] 
    private GameObject meteorButton;
    [SerializeField] 
    private GameObject piercingButton;
    [SerializeField] 
    private GameObject invisibleButton;
    [SerializeField] 
    private GameObject sparkButton;
    [SerializeField] 
    private GameObject shadowButton;
    [SerializeField] 
    private GameObject iceButton;
    [SerializeField] 
    private GameObject rangeButton;
    [SerializeField] 
    private GameObject criticalButton;
    [SerializeField] 
    private GameObject doubleButton;
    [SerializeField] 
    private GameObject lifeButton;
    [SerializeField] 
    private GameObject damageButton;
    [SerializeField] 
    private GameObject hpButton;
    [SerializeField] 
    private GameObject speedButton;

    

    private GameObject[] newButtons1; 
    private GameObject[] newButtons2; 
    private GameObject[] newButtons3; 

    //-----------Buttons-------------//


    [SerializeField] 
    public Slider hpBar;

    [SerializeField] 
    private Slider expBar;
    //-----------Objects-------------//


    
    void Awake() {
        abilityButtons1 = new GameObject[] { hpButton, sizeupButton, meteorButton, piercingButton, invisibleButton };
        abilityButtons2 = new GameObject[] { asButton, speedButton, sparkButton, shadowButton, iceButton };
        abilityButtons3 = new GameObject[] { damageButton, criticalButton, doubleButton, lifeButton, rangeButton };

        newButtons1 = abilityButtons1;
        newButtons2 = abilityButtons2;
        newButtons3 = abilityButtons3;
        if (instance == null){
            instance = this;
            done = true;
        }
        
        foreach(GameObject button in abilityButtons1){
        button.SetActive(false);
        }
        foreach(GameObject button in abilityButtons2){
        button.SetActive(false);
        }  
        foreach(GameObject button in abilityButtons3){
        button.SetActive(false);
        } 
        bulletShootInterval = 0.5f;
        // bools
        shadowMove = true;
        checkSpark = false;
        isPiercing = false;
        doubleShooting = false;
        isLifeSteal = false;
        isInvisible = false;
        isMeteor = false;
        isIce = false;
        isSizeup = false;
        
    }


    void Update()
        {
        direction = Random.Range(0, 8);
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.shoot();
        if (checkSpark == true){
            player.sparking();
        }

        if (done == false){
            reset();
        }
    }

    public void pause(){
        if (paused == false){
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            paused = true;
        }
        else{
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            paused = false;
        }
    }
    public void getScore(){
        int increaseScore = 5;
        score += increaseScore;
        EnemySpawner spawner = gameObject.GetComponent<EnemySpawner>();
        // if (spawner.spawnCount % 60 == 0){
        //     increaseScore += 11;
        // }
        text.SetText(score.ToString("D10"));
    }
    public void GetDamage(){
        RectTransform hpBarSize = hpBar.GetComponent<RectTransform>();
        hp += armor;
        hpBarSize.offsetMax = new Vector2(-hp, hpBarSize.offsetMax.y);
        
    }

    public void updateHpBar(){
        RectTransform hpBarSize = hpBar.GetComponent<RectTransform>();
        hpBarSize.offsetMax = new Vector2(-hp, hpBarSize.offsetMax.y);
    }
    public void GetEXP(){
        RectTransform expBarSize = expBar.GetComponent<RectTransform>();
        exp -= addExp;
        expBarSize.offsetMax = new Vector2(-exp, expBarSize.offsetMax.y);
        
        if (exp <= 0){
            LevelUp();
            addExp -= 0.03f;
            expBarSize.offsetMax = new Vector2(-exp, expBarSize.offsetMax.y);
        }
    }
    private void LevelUp(){
        level++;
        hp = -armor;
        exp = 450f;  
        GetDamage();
        Time.timeScale = 0;
        abilityPanel.SetActive(true);
        int randomIndex1 = Random.Range(0, abilityButtons1.Length);
        int randomIndex2 = Random.Range(0, abilityButtons2.Length);
        int randomIndex3 = Random.Range(0, abilityButtons3.Length);
        abilityButtons1[randomIndex1].SetActive(true); 
        abilityButtons2[randomIndex2].SetActive(true);
        abilityButtons3[randomIndex3].SetActive(true);
        Debug.Log("level: " + level);
    }

    private void resumeGame(){
        foreach(GameObject button in abilityButtons1){
        button.SetActive(false);
        }
        foreach(GameObject button in abilityButtons2){
        button.SetActive(false);
        }  
        foreach(GameObject button in abilityButtons3){
        button.SetActive(false);
        } 
        abilityPanel.SetActive(false);
        Time.timeScale = 1;
    }
    //-------------Ability---------------//

    private void RemoveElementFromArray(GameObject[] array, GameObject elementToRemove)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == elementToRemove)
            {
                // 해당 요소를 제거하려면 null로 설정합니다.
                array[i] = null;
                break;
            }
        }
    }

    // 배열에서 null 요소를 정리하는 함수
    private GameObject[] CleanUpArray(GameObject[] array)
    {
        int nullCount = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                nullCount++;
            }
        }

        GameObject[] newArray = new GameObject[array.Length - nullCount];
        int newIndex = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                newArray[newIndex] = array[i];
                newIndex++;
            }
        }

        return newArray;
    }
    

    
    public void getHp(){
        armor -= 5f;
        resumeGame();

    }
    public void speedUp(){
        moveSpeed += 0.5f;
        resumeGame();
    }
    public void increaseDamage(){
        damage += 0.5f;
        resumeGame();
    }

    public void getSpark(){
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        checkSpark = true;

        sparkButton.SetActive(false);
        RemoveElementFromArray(abilityButtons2, sparkButton);
        abilityButtons2 = CleanUpArray(abilityButtons2);
        resumeGame();
    }
    
    public void increaseCriticalPercent(){
        criticalPercent += 5;
        if (criticalPercent >= 100){
            criticalButton.SetActive(false);
            RemoveElementFromArray(abilityButtons3, criticalButton);
            abilityButtons3 = CleanUpArray(abilityButtons3);
        }
        resumeGame();
        }


    
    public void increaseRange(){
        Bullet bullet = GameObject.FindGameObjectWithTag("Bullet").GetComponent<Bullet>();
        bullet.upRange();
        
        range++;
        if (range >= 6){
            rangeButton.SetActive(false);
            RemoveElementFromArray(abilityButtons3, rangeButton);
            abilityButtons3 = CleanUpArray(abilityButtons3);
        }
        resumeGame();
    }

    public void increaseAs(){
        bulletShootInterval -= 0.01f;
        if (bulletShootInterval <= 0.01f){
            asButton.SetActive(false);
            RemoveElementFromArray(abilityButtons2, asButton);
            abilityButtons2 = CleanUpArray(abilityButtons2);
        }
        resumeGame();
    }

    public void doubleShot(){
        doubleShooting = true;
        doubleButton.SetActive(false);
        RemoveElementFromArray(abilityButtons3, doubleButton);
        abilityButtons3 = CleanUpArray(abilityButtons3);
        resumeGame();
    }

    public void getPiercing(){
        isPiercing = true;
        piercingButton.SetActive(false);
        RemoveElementFromArray(abilityButtons1, piercingButton);
        abilityButtons1 = CleanUpArray(abilityButtons1);
        resumeGame();
    }
    public void getLifeSteal(){
        isLifeSteal = true;
        lifeButton.SetActive(false);
        RemoveElementFromArray(abilityButtons3, lifeButton);
        abilityButtons3 = CleanUpArray(abilityButtons3);
        resumeGame();
    }

    public void getInvisible(){
        isInvisible = true;
        invisibleButton.SetActive(false);
        RemoveElementFromArray(abilityButtons1, invisibleButton);
        abilityButtons1 = CleanUpArray(abilityButtons1);
        resumeGame();
    }

    public void getMeteor(){
        isMeteor = true;
        meteorButton.SetActive(false);
        RemoveElementFromArray(abilityButtons1, meteorButton);
        abilityButtons1 = CleanUpArray(abilityButtons1);
        iceButton.SetActive(false);
        RemoveElementFromArray(abilityButtons2, iceButton);
        abilityButtons2 = CleanUpArray(abilityButtons2);
        resumeGame();
    }
    public void getIceAge(){
        isIce = true;
        meteorButton.SetActive(false);
        RemoveElementFromArray(abilityButtons1, meteorButton);
        abilityButtons1 = CleanUpArray(abilityButtons1);
        iceButton.SetActive(false);
        RemoveElementFromArray(abilityButtons2, iceButton);
        abilityButtons2 = CleanUpArray(abilityButtons2);
        resumeGame();
    }
    public void getSizeup(){
        isSizeup = true;
        sizeupButton.SetActive(false);
        RemoveElementFromArray(abilityButtons1, sizeupButton);
        abilityButtons1 = CleanUpArray(abilityButtons1);
        resumeGame();
    }
    public void getShadow(){
        Player players = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        players.instantiateShadows();
        shadowButton.SetActive(false);
        RemoveElementFromArray(abilityButtons2, shadowButton);
        abilityButtons2 = CleanUpArray(abilityButtons2);
        resumeGame();
    }




    void reset(){
        abilityButtons1 = newButtons1;
        abilityButtons2 = newButtons2;
        abilityButtons3 = newButtons3;
    }
}


