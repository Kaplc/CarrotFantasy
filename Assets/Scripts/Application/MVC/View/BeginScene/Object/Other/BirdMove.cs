using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public float speed;
    public float time;
    private float nowTime;

    private void Awake()
    {
        nowTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTime > 0)
        {
            transform.Translate(transform.up * (speed * Time.deltaTime));
            nowTime -= Time.deltaTime;
        }
        else
        {
            nowTime = time;
            speed = -speed;
        }
    }
}
