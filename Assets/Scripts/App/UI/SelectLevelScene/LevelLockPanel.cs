using UnityEngine;
using UnityEngine.UI;

namespace App.UI.SelectLevelScene
{
    public class LevelLockPanel : MonoBehaviour
    {
        public Button btnSure;
        public Button btnClose;

        private void Start()
        {
            btnClose.onClick.AddListener(() => { gameObject.SetActive(false); });
            btnSure.onClick.AddListener(() => { gameObject.SetActive(false); });
        }
    }
}