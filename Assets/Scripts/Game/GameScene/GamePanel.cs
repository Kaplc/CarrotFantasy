using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Button btnPause;
    public Button btnContinue;
    public Button btnMenu;
    public Text txGem;
    public Text txNowWave;
    public Text txTotalWaves;
    public Image imgPause;
    
    protected override void Init()
    {
        btnSpeed1.onClick.AddListener(() =>
        {
            btnSpeed2.gameObject.SetActive(true);
            btnSpeed1.gameObject.SetActive(false);
        });
        btnSpeed2.onClick.AddListener(() =>
        {
            btnSpeed1.gameObject.SetActive(true);
            btnSpeed2.gameObject.SetActive(false);
        });
        btnPause.onClick.AddListener(() =>
        {
            btnContinue.gameObject.SetActive(true);
            btnPause.gameObject.SetActive(false);
        });
        btnContinue.onClick.AddListener(() =>
        {
            btnPause.gameObject.SetActive(true);
            btnContinue.gameObject.SetActive(false);
        });
        btnMenu.onClick.AddListener(() =>
        {
            
        });
        
        btnContinue.gameObject.SetActive(false);
        btnSpeed2.gameObject.SetActive(false);
        imgPause.gameObject.SetActive(false);
    }
    
}
