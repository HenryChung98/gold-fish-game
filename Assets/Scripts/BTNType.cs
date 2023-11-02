using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonType : MonoBehaviour
{

    public BTNType currentType;
    public void OnBtnClick(){
        switch(currentType){
            case BTNType.Start:
                SceneManager.LoadScene("GameScene");
                break;
            case BTNType.Option:
                Debug.Log("Option");
                break;
            case BTNType.Quit:
                Application.Quit();
                break;
            case BTNType.Back:
                SceneManager.LoadScene("MainMenuScene");
                break;
        }
    }
}
