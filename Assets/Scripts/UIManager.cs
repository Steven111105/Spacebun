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
    
    private void OnEnable()
    {
        currHealth = maxHealth;
        SetHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
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
