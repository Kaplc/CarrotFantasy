using UnityEngine;
using UnityEngine.UI;

namespace App.MVC.View.BeginScene.UI.Panel.BeginPanel.SettingPanel
{
    public class ToggleExtend : MonoBehaviour
    {
        public Image imgBackGround;
        private Toggle tg;

        private void Start()
        {
            tg = GetComponent<Toggle>();
        
            tg.onValueChanged.AddListener((isOn)=>
            {
                imgBackGround.enabled = !isOn;
            });
        
            // toggle默认为开启
            imgBackGround.enabled = !tg.isOn;
        }
    }
}
