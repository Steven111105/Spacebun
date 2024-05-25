using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public void Blink(int type){
        StopAllCoroutines();
        StartCoroutine(BlinkRoutine(type));
    }
    
    IEnumerator BlinkRoutine(int type){
        Color targetColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(type == 0){
            targetColor = Color.red;
        }else if(type == 1){
            targetColor = Color.blue;
        }else{
            targetColor = Color.green;
        }
        for(int i = 0; i < 5; i++){
            sr.color = targetColor;
            yield return new WaitForSeconds(0.2f);
            //change sprite back to default
            sr.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        sr.color = targetColor;
        yield return new WaitForSeconds(5f);
        sr.color = Color.white;

    }
}
