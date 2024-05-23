using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator mainMenuAnim;
    public TMP_Text highscoreText;
    public TMP_Text carrotsText;
    public bool unlockedLvl2;
    public bool unlockedLvl3;

    public int[] highscore = new int[3];

    private void OnEnable()
    {
        // PlayerPrefs.SetInt("HighScore1", 10);
        // PlayerPrefs.SetInt("HighScore2", 20);
        // PlayerPrefs.SetInt("HighScore3", 30);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Carrots", 10);
        mainMenuAnim.SetInteger("Level", 0);
        highscore[0] = PlayerPrefs.GetInt("HighScore1", 0);
        highscore[1] = PlayerPrefs.GetInt("HighScore2", 0);
        highscore[2] = PlayerPrefs.GetInt("HighScore3", 0);
        if(highscore[0] > 100000){
            unlockedLvl2 = true;
        }
        if(highscore[1] > 100000){
            unlockedLvl3 = true;
        }
        highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("Level")];
        carrotsText.text = "Carrots: " + PlayerPrefs.GetInt("Carrots", 0);
    }
    public void LevelSelect(){
        Debug.Log("Level Select");
        mainMenuAnim.SetTrigger("LevelSelect");
    }
    public void Play(){
        if(unlockedLvl2 && unlockedLvl3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuAnim.GetInteger("Level") + 1);
        }else{
            if(mainMenuAnim.GetInteger("Level") == 1 && unlockedLvl2){
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }else if(mainMenuAnim.GetInteger("Level") == 2 && unlockedLvl3){
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }
        
    }

    public void NextLvl(){
        if(mainMenuAnim.GetInteger("Level") != 2){
            mainMenuAnim.SetInteger("Level", mainMenuAnim.GetInteger("Level") + 1);;
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("Level")];
        }else{
            mainMenuAnim.SetInteger("Level", 0);
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("Level")];
        }
    }
    public void PrevLvl(){
        if(mainMenuAnim.GetInteger("Level") != 0){
            mainMenuAnim.SetInteger("Level", mainMenuAnim.GetInteger("Level") - 1);;
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("Level")];
        }else{
            mainMenuAnim.SetInteger("Level", 2);
            highscoreText.text = "High Score: " + highscore[mainMenuAnim.GetInteger("Level")];
        }
    }
    public void Back(){
        mainMenuAnim.SetTrigger("GoToMenu");
    }
    public void Settings(){
        mainMenuAnim.SetTrigger("Settings");
    }
    public void Quit(){
        Application.Quit();
    }
}
