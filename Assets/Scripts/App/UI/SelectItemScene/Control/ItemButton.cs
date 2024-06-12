using UnityEngine;
using UnityEngine.UI;

namespace App.UI.SelectItemScene.Control
{
    public class ItemButton : MonoBehaviour
    {
        public Image imgLock;
        public Image imgUnlockMapCount;
        public Text txUnlockMapCount;
    
        private bool IsLock
        {
            set
            {
                imgLock.gameObject.SetActive(!value);
                imgUnlockMapCount.gameObject.SetActive(value);
            }
        }

        public void UpdateUnlockMapCount(int unlockCount)
        {
            IsLock = true;
            txUnlockMapCount.text = $"{unlockCount + 1}/9";
        }
    }
}
