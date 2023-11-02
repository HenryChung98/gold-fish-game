using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideAreaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meteor;
    [SerializeField]
    private GameObject ice;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(meteors());
        StartCoroutine(ices());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator meteors(){
            while (true){
                if (GameManager.instance.isMeteor == true){
                for (int i = 0; i <= 7; i++){
                float randomAttack = Random.Range(-9, 3);
                Vector3 attackPos = new Vector3(randomAttack, 7, transform.position.z);
                Instantiate(meteor, attackPos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(10f);
                }
                else{
                    yield return null;
                }
                }
            }

    private IEnumerator ices(){
            while (true){
                if (GameManager.instance.isIce == true){
                for (int i = 0; i <= 7; i++){
                float randomAttack = Random.Range(-9, 3);
                Vector3 attackPos = new Vector3(randomAttack, 7, transform.position.z);
                Instantiate(ice, attackPos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(10f);
                }
                else{
                    yield return null;
                }
                }
            }
}
