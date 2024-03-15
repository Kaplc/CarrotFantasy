using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : BasePanel
{
    public Text text;
    public Button button;

    protected override void Init()
    {
        button.onClick.AddListener(() =>
        {

            // GameManager.Instance.sdkManager.Dispose();
        });
    }

    public void SetInfo(string info)
    {
        text.text = info;
        StartCoroutine(ClosePanelCoroutine());
    }

    private IEnumerator ClosePanelCoroutine()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.Hide<TipsPanel>();
    }
}
