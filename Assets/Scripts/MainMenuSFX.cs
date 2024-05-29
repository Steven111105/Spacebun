using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSFX : MonoBehaviour
{
    public AudioClip[] sfx;
    //0 = button click
    //1 = change lvl
    //2 = play
    public AudioSource audioSource;
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ButtonClick()
    {
        audioSource.PlayOneShot(sfx[0]);
    }
    public void ChangeLvl()
    {
        audioSource.PlayOneShot(sfx[1]);
    }
    public void Play()
    {
        audioSource.PlayOneShot(sfx[2]);
    }
}
