using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public Animator mainMenuAnim;
    public GameObject levelSelect;

    public void NextLvl(){
        mainMenuAnim.SetInteger("LevelSelect", mainMenuAnim.GetInteger("LevelSelect") + 1);;
    }
    public void PrevLvl(){
        mainMenuAnim.SetInteger("LevelSelect", mainMenuAnim.GetInteger("LevelSelect") - 1);;
    }
    public void Back(){
        // levelSelect.SetActive(false);
        mainMenuAnim.SetTrigger("Back");
    }
    public void Settings(){
        mainMenuAnim.SetTrigger("Settings");
    }
    public void Quit(){
        Application.Quit();
    }
}
