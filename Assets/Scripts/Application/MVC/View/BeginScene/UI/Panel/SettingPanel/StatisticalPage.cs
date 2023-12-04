using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticalPage : MonoBehaviour
{
    public Text txAdventureMap;
    public Text txHideMap;
    public Text txBossMap;
    public Text txMoney;
    public Text txKillMonsterCount;
    public Text txKillBossCount;
    public Text txDestroyItemCount;

    public void UpdateDate(StatisticalData data)
    {
        txAdventureMap.text = data.adventureMapCount.ToString();
        txHideMap.text = data.hideMapCount.ToString();
        txBossMap.text = data.bossMapCount.ToString();
        txMoney.text = data.money.ToString();
        txKillMonsterCount.text = data.killMonsterCount.ToString();
        txKillBossCount.text = data.killBossCount.ToString();
        txDestroyItemCount.text = data.destroyItemCount.ToString();
    }
}
