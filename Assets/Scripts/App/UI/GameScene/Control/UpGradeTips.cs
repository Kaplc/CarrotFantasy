using GameFramework;
using UnityEngine;

namespace App.UI.GameScene.Control
{
    public class UpGradeTips : MonoBehaviour, IPoolObject
    {
        public Animator animator;

        public void OnGet()
        {
            animator.enabled = true;
        }

        public void OnPush()
        {
            animator.enabled = false;
        }
    }
}