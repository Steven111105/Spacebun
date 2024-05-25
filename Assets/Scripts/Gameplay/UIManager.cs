using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public TMP_Text carrotText;
    public GameObject gameOverPanel;
    int maxHealth = 3;
    int currHealth;
    int score;
    int carrots;
    int prevCarrots;
    
    private void OnEnable()
    {
        Time.timeScale = 1;
        //carrot get from the save
        carrots = PlayerPrefs.GetInt("Carrots", 0);
        prevCarrots = carrots;
        currHealth = maxHealth;
        SetHealth(maxHealth);
        gameOverPanel.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        carrotText.text = "Carrots: " + carrots;
    }
    private void Update()
    {
        if(currHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int addedScore){
        score += addedScore;
        scoreText.text = "Score: " + score;

    }
    public void AddCarrot(int addedCarrot){
        carrots += addedCarrot;
        carrotText.text = "Carrots: " + carrots;
        PlayerPrefs.SetInt("Carrots", carrots);
    }
    public void SetHealth(int targetHealth){
        healthText.text = "Health: " + targetHealth;
    }

    public void MinusHealth()
    {
        Debug.Log("minus health");
        currHealth--;
        healthText.text = "Health: " + currHealth;
    }

    public void BackToMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Skor: " + score;
        gameOverPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "Tambahan Wortel +" + (carrots - prevCarrots);
        gameOverPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Total Wortel " + carrots;
        //get scene index
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if(score > PlayerPrefs.GetInt("HighScore" + sceneIndex, 0))
        {
            //save highscore on "Highscore" + sceneIndex
            PlayerPrefs.SetInt("HighScore" + sceneIndex, score);
        }
        PlayerPrefs.SetInt("Carrots", carrots);
        gameOverPanel.SetActive(true);
    }
    public void Replay(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
