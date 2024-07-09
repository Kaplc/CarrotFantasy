using App.Game;
using GameFramework;
using TMPro;
using UnityEngine;

namespace App.UI.GameScene.Control
{
    public class AddMoneyTips : MonoBehaviour, IPoolObject
    {
        public Animator animator;
        public TextMeshPro textMeshPro;

        public void OnGet()
        {
            animator.enabled = true;
        }

        public void OnPush()
        {
            animator.enabled = false;
        }

        public void PushSelf()
        {
            GameManager.Instance.factoryManager.UIControlFactory.PushControl(gameObject);
        }
    }
}