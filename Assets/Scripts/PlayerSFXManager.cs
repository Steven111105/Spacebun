using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    //0 = dash
    //1 = pickup
    //2 = Hurt
    
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
