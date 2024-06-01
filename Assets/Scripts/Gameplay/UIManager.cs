using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public AudioClip buttonSFX;
    public AudioSource audioSource;
    public UnityEvent pause;
    public UnityEvent unpause;
    public UnityEvent gameOver;
    public AudioSource BGM;
    public GameObject hearts;
    public TMP_Text scoreText;
    public TMP_Text carrotText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
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
        SetHealth();
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        score = 0;
        scoreText.text = score.ToString();
        carrotText.text = carrots.ToString();
        audioSource = GetComponent<AudioSource>();
    }
    public void ButtonClick(){
        audioSource.PlayOneShot(buttonSFX);
    }
    private void Update()
    {
        if(currHealth <= 0)
        {
            GameOver();
        }
        //

        if(Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    }
    
    public void Pause(){
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        BGM.Pause();
        pause.Invoke();
    }

    public void Unpause(){
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        BGM.UnPause();
        unpause.Invoke();
    }

    public void AddScore(int addedScore){
        score += addedScore;
        scoreText.text = score.ToString();

    }
    public void AddCarrot(int addedCarrot){
        carrots += addedCarrot;
        carrotText.text = carrots.ToString();
        PlayerPrefs.SetInt("Carrots", carrots);
    }
    public void SetHealth(){
        for(int i = 0; i < 3; i++){
            hearts.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void MinusHealth()
    {
        currHealth--;
        hearts.transform.GetChild(currHealth).gameObject.SetActive(false);
    }

    public void BackToMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        BGM.Stop();
        gameOver.Invoke();
        if(score > PlayerPrefs.GetInt("HighScore" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, 0))
        {
            //save highscore on "Highscore" + sceneIndex
            PlayerPrefs.SetInt("HighScore" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, score);
        }
        gameOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Skor: " + score;
        gameOverPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "Skor Tertinggi: " + PlayerPrefs.GetInt("HighScore" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, 0);
        gameOverPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "Tambahan Wortel +" + (carrots - prevCarrots);
        gameOverPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = "Total Wortel " + carrots;
        //get scene index
        PlayerPrefs.SetInt("Carrots", carrots);
        gameOverPanel.SetActive(true);
    }
    public void Replay(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void Shop(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void Exit(){
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
