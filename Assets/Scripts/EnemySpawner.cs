using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject[] boss;
    [SerializeField] 
    private GameObject hpFood;
    private int enemyIndex = 0;
    private int bossIndex = 0;
    [SerializeField]
    private GameObject food;
    
    
    private float foodMinY = -4.5f;
    private float enemyMinY = -4f;
    private float foodMaxY = 4.5f;
    private float enemyMaxY = 4f;
    private float foodSpawnInterval = 3f;
    private float enemySpawnInterval = 2.5f;
    public int spawnCount = 0; // 7번당 배경 한번



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFoodRoutine());
        StartCoroutine(SpawnEnemyRoutine());

    }

    IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {

            float foodRandomY = Random.Range(foodMinY, foodMaxY);

            SpawnFood(foodRandomY);

            yield return new WaitForSeconds(foodSpawnInterval);
        }
        }
    IEnumerator SpawnEnemyRoutine()
    {
        
        
        while (true)
        {
            float enemyRandomY = Random.Range(enemyMinY, 0f);
            float enemyRandomTwo = Random.Range(0f, enemyMaxY);
            SpawnEnemy(enemyRandomY, enemyRandomTwo, enemyIndex);

            //float enemyRandomY2 = Random.Range(enemyMinY, enemyMaxY);
            //SpawnEnemy(enemyRandomY2, enemyIndex);
     
            spawnCount++;
            if (spawnCount % 50 == 0){
                enemySpawnInterval -= 0.1f;
            }
            if (spawnCount % 70 == 0){
                enemyIndex++;
                
                
                }
            if (spawnCount % 150 == 0){
                SpawnBoss(bossIndex);
                bossIndex++;
                // if (bossIndex >= boss.Length - 1){
                //     bossIndex = 0;
                //     Boss bossHp = gameObject.GetComponent<Boss>();
                //     bossHp.hp *= 1.5f;  //-----------------완성직전 바꿀것 
                // }

            }
            if (enemyIndex >= enemies.Length){
                    enemyIndex = 0;
                    Enemy enemyHp = gameObject.GetComponent<Enemy>();
                    //enemyHp.hp *= 2f;  //-----------------완성직전 바꿀것 
                }
            yield return new WaitForSeconds(enemySpawnInterval);
            
        }
    }

    void SpawnFood(float posY)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posY, transform.position.z);
        Instantiate(food, spawnPos, Quaternion.identity);
        int hpFoodInstan = Random.Range(0, 100);
        if (hpFoodInstan <= 5){
            Instantiate(hpFood, spawnPos, Quaternion.identity);
        }
    }
    void SpawnEnemy(float posY, float posTwo, int index)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posY, transform.position.z);
        Vector3 spawnPosTwo = new Vector3(transform.position.x, posTwo, transform.position.z);
        if (spawnCount >= 70){
            int twoRandom = Random.Range(0, 2);
            if (twoRandom == 0){
                Instantiate(enemies[index], spawnPos, Quaternion.identity);
            }
            else if (twoRandom == 1){
                Instantiate(enemies[index - 1], spawnPosTwo, Quaternion.identity);
            }
            
        }
        else{
        Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Instantiate(enemies[index], spawnPosTwo, Quaternion.identity);
        }
        
        
        
        
    }
    void SpawnBoss(int index)
    {
        Vector3 spawnPos = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        Instantiate(boss[index], spawnPos, Quaternion.identity);
    }
}