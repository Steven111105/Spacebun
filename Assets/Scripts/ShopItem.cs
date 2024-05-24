using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    //upgrade 0 = zebra cross for blind
    //upgrade 1 = Warning lights
    //upgrade 2 = speed bump
    //upgrade 3 = bubble thing
    public int type;
    public int price;

    private void OnEnable()
    {
        
    }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Carrots") >= price)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - price);
            PlayerPrefs.SetInt("Upgrade" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+ type, 1);
        }
    }


}
