using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPage : MonoBehaviour
{
    public Text txAdventureMap;
    public Text txHideMap;
    public Text txBossMap;
    public Text txMoney;
    public Text txKillMonsterCount;
    public Text txKillBossCount;
    public Text txDestroyItemCount;

    public void UpdateAdventureMap(int count)
    {
        txAdventureMap.text = count.ToString();
    }
    
    public void UpdateHideMap(int count)
    {
        txHideMap.text = count.ToString();
    }
    public void UpdateBossMap(int count)
    {
        txBossMap.text = count.ToString();
    }
    public void UpdateMoney(int count)
    {
        txMoney.text = count.ToString();
    }
    public void UpdateKillMonsterCount(int count)
    {
        txKillMonsterCount.text = count.ToString();
    }
    public void UpdateKillBossCount(int count)
    {
        txKillBossCount.text = count.ToString();
    }

    public void UpdateDestroyItemCount(int count)
    {
        txDestroyItemCount.text = count.ToString();
    }
}
