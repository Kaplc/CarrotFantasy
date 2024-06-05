using DG.Tweening;
using UnityEngine;

namespace App.MVC.View.BeginScene.Object.Other
{
    public class Bird : MonoBehaviour
    {
        public float time;
        private Tween tween;
    
        private void Start()
        {
            //创建移动动画
            tween = transform.DOLocalMoveY(((RectTransform)transform).anchoredPosition.y + 50f, time);
            // 设置循环移动
            tween.SetLoops(-1, LoopType.Yoyo); 
        }

        private void OnDestroy()
        {
            tween?.Kill();
        }
    }
}
