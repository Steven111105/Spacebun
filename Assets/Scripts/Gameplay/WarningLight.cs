using System.Collections;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public Sprite targetSprite;
    [SerializeField]
    Sprite defaultSprite;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    public void Blink(int type){
        StopAllCoroutines();
        StartCoroutine(BlinkRoutine(type));
    }
    
    IEnumerator BlinkRoutine(int type){
        targetSprite = transform.GetComponentInParent<LightManager>().sprites[type];
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for(int i = 0; i < 5; i++){
            sr.sprite = targetSprite;
            yield return new WaitForSeconds(0.2f);
            //change sprite back to default
            sr.sprite = defaultSprite;
            yield return new WaitForSeconds(0.2f);
        }
        sr.sprite = targetSprite;
        yield return new WaitForSeconds(3f);
        sr.sprite = defaultSprite;
    }
}
