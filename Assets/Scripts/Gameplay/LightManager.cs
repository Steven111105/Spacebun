using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    public void Blink(int direction, int type){
        transform.GetChild(direction).GetComponent<WarningLight>().Blink(type);
    }
}
