using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    public static MainMenuBGM audiomanagerInstance;
    private void Awake()
    {
        if(audiomanagerInstance != null && audiomanagerInstance != this)
        {
            Destroy(gameObject);
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
            Destroy(gameObject);
            return;
        }
    }
}
