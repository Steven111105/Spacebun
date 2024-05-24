using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int type;
    public int price;

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Carrots") >= price)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - price);
            PlayerPrefs.SetInt("Upgrade" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+ type, 1);
        }
    }


}
