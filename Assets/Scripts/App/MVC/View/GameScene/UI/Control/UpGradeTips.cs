using Library;
using UnityEngine;

namespace App.MVC.View.GameScene.UI.Control
{
    public class UpGradeTips: MonoBehaviour, IPoolObject
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
