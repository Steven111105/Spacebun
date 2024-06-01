using System.Collections;
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
    public bool hasSeenTutorial;
    public GameObject maps;

    private void OnEnable()
    {
        Time.timeScale = 1;
        mainMenuAnim.SetInteger("Level", 0);
        highscore[0] = PlayerPrefs.GetInt("HighScore1", 0);
        highscore[1] = PlayerPrefs.GetInt("HighScore2", 0);
        highscore[2] = PlayerPrefs.GetInt("HighScore3", 0);
        hasSeenTutorial = PlayerPrefs.GetInt("HasSeenTutorial", 0) == 1;
        if(highscore[0] > 3000){
            unlockedLvl2 = true;
            maps.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        }
        if(highscore[1] > 3000){
            unlockedLvl3 = true;
            maps.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        }
        highscoreText.text = "Skor Tertinggi: " + highscore[mainMenuAnim.GetInteger("Level")];
        carrotsText.text = PlayerPrefs.GetInt("Carrots", 0).ToString();
    }
    public void LevelSelect(){
        if(!hasSeenTutorial){
            hasSeenTutorial = true;
            gameObject.GetComponent<HowToPlay>().StartPages(true);
        }else{
            mainMenuAnim.SetTrigger("LevelSelect");
        }
    }
    public void PlayButton(){
        if(unlockedLvl2 && unlockedLvl3){
            StartCoroutine(PlayDelay());
        }else{
            if(mainMenuAnim.GetInteger("Level") == 0){
                StartCoroutine(PlayDelay());
            }else if(mainMenuAnim.GetInteger("Level") == 1 && unlockedLvl2){
                StartCoroutine(PlayDelay());
                
            }else if(mainMenuAnim.GetInteger("Level") == 2 && unlockedLvl3){
                StartCoroutine(PlayDelay());
            }else{
                GetComponent<MainMenuSFX>().Locked();
            }
        }
    }
    IEnumerator PlayDelay(){
        GetComponent<MainMenuSFX>().Play();
        yield return new WaitForSeconds(0.5f);
        Play();
    }
    void Play(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuAnim.GetInteger("Level") + 4); 
    }

    public void NextLvl(){
        Debug.Log("Next");
        if(mainMenuAnim.GetInteger("Level") != 2){
            mainMenuAnim.SetInteger("Level", mainMenuAnim.GetInteger("Level") + 1);;
            highscoreText.text = "Skor Tertinggi: " + highscore[mainMenuAnim.GetInteger("Level")];
        }else{
            mainMenuAnim.SetInteger("Level", 0);
            highscoreText.text = "Skor Tertinggi: " + highscore[mainMenuAnim.GetInteger("Level")];
        }
    }
    public void PrevLvl(){
        Debug.Log("Prev");
        if(mainMenuAnim.GetInteger("Level") != 0){
            mainMenuAnim.SetInteger("Level", mainMenuAnim.GetInteger("Level") - 1);;
            highscoreText.text = "Skor Tertinggi: " + highscore[mainMenuAnim.GetInteger("Level")];
        }else{
            mainMenuAnim.SetInteger("Level", 2);
            highscoreText.text = "Skor Tertinggi: " + highscore[mainMenuAnim.GetInteger("Level")];
        }
    }
    public void ResetSave(){
        PlayerPrefs.DeleteAll();
        StartCoroutine(ResetSaveDelay());
    }
    IEnumerator ResetSaveDelay(){
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Back(){
        Debug.Log("Back");
        mainMenuAnim.SetTrigger("GoToMenu");
    }
    public void Settings(){
        Debug.Log("Settings");
        mainMenuAnim.SetTrigger("Settings");
    }
    public void Quit(){
        Application.Quit();
    }
}
