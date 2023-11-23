using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownPanel : MonoBehaviour
{
    public float speed;
    public Image imgCount;
    public Transform imgFire;
    public List<Sprite> countDownImage;
    private int index;

    private void Start()
    {
        index = countDownImage.Count - 1;
        StartCoroutine(CountDownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        imgFire.Rotate(Vector3.forward, Time.deltaTime * speed);
    }

    private IEnumerator CountDownCoroutine()
    {
        while (index >= 0)
        {
            imgCount.sprite = countDownImage[index];

            yield return new WaitForSeconds(1f);
            index--;
        }
        gameObject.SetActive(false);
        // 开始出怪
        GameManager.Instance.EventCenter.TriggerEvent(NotificationName.START_SPAWN);
    }
}
