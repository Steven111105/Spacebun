using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator mainMenuAnim;
    public TMP_Text highscoreText;
    public int[] highscore = new int[3];

    public void NextLvl(){
        if(mainMenuAnim.GetInteger("LevelSelect") != 3){
            mainMenuAnim.SetInteger("LevelSelect", mainMenuAnim.GetInteger("LevelSelect") + 1);;
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("LevelSelect")-1];
        }
    }
    public void PrevLvl(){
        mainMenuAnim.SetInteger("LevelSelect", mainMenuAnim.GetInteger("LevelSelect") - 1);;
        if(mainMenuAnim.GetInteger("LevelSelect") != 0){
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("LevelSelect")-1];
        }
    }
    public void Back(){
        // levelSelect.SetActive(false);
        mainMenuAnim.SetTrigger("Back");
    }
    public void Settings(){
        mainMenuAnim.SetTrigger("Settings");
    }
    public void Quit(){
        Application.Quit();
    }
}
