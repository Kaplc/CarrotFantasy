using UnityEngine;
using UnityEngine.UI;

namespace App.UI.BeginScene.SettingPanel
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
                print(isOn);
                if (isOn)
                {
                    imgBackGround.enabled = false;
                }
                else
                {
                    imgBackGround.enabled = true;
                }
            });
        
            // toggle默认为开启
            imgBackGround.enabled = !tg.isOn;
        }
    }
}
