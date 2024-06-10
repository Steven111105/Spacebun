using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSFXManager : MonoBehaviour
{
    //0 = buy
    //1 = not enough money
    //2 = play 
    //3 = next button for story
    public AudioClip[] sfx;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BuySFX(){
        audioSource.Stop();
        audioSource.PlayOneShot(sfx[0]);
    }

    public void NotEnoughMoney(){
        audioSource.PlayOneShot(sfx[1]);
    }

    public void NextButton(){
        audioSource.PlayOneShot(sfx[3]);
    }

    public void Play(){
        audioSource.PlayOneShot(sfx[2]);
    }
}
