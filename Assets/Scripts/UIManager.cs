using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public TMP_Text carrotText;
    int maxHealth = 3;
    int currHealth;
    int score;
    
    private void OnEnable()
    {
        //carrot get from the save
        currHealth = maxHealth;
        SetHealth(maxHealth);
        score = 0;

    }

    public void AddScore(int addedScore){
        score += addedScore;
        scoreText.text = "Score: " + score;

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
}
