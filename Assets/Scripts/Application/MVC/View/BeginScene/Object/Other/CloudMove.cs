using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;
    public float time;
    private float nowTime;
    private Vector3 originPos;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        originPos = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        nowTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTime > 0)
        {
            transform.Translate(transform.right * (speed * Time.deltaTime));
            nowTime -= Time.deltaTime;
        }
        else
        {
            nowTime = time;
            ((RectTransform)transform).anchoredPosition = originPos;
        }
    }
}
