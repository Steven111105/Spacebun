using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    public static MainMenuBGM audiomanagerInstance;
    private void Awake()
    {
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        if(audiomanagerInstance != null && audiomanagerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        audiomanagerInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 1 && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 3)
        {
            Debug.Log("Destroying BGM");
            Destroy(this.gameObject);
            return;
        }
    }
}
