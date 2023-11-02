using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnAdventure;
    public Button btnBoss;
    public Button btnMonster;

    protected override void Init()
    {
        btnAdventure.onClick.AddListener(() =>
        {
            
        });
        btnBoss.onClick.AddListener(() =>
        {
            
        });
        btnMonster.onClick.AddListener(() =>
        {
            
        });
    }
}
