using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSFXManager : MonoBehaviour
{
    //0 = buy
    //1 = sell
    //2 = not enough money
    public AudioClip[] sfx;
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(int index)
    {
        audioSource.Stop();
        audioSource.clip = sfx[index];
        audioSource.Play();
    }
}
