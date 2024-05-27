using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCross : MonoBehaviour
{
    public Vector2 direction;
    public GameObject endTrigger;
    public bool blindStop;
    private void OnEnable()
    {
        direction = new Vector2(endTrigger.transform.position.x - transform.position.x, endTrigger.transform.position.y - transform.position.y).normalized;
    }
}
