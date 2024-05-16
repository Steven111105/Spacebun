using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int carType;
    public float movespeed;
    public Vector2 direction;
    Rigidbody2D rb;

    private void OnEnable()
    {
        if(carType == 0)
        {
            movespeed = 3;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if(carType == 1)
        {
            movespeed = 5;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * movespeed;
    }
}
