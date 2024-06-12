using DG.Tweening;
using UnityEngine;

namespace App.MVC.View.BeginScene.Object.Other
{
    public class Cloud : MonoBehaviour
    {
        public float time;
        private Tween tween;
    
        private void Start()
        {
            tween = transform.DOLocalMoveX(((RectTransform)transform).anchoredPosition.x + 1920f, time);
            tween.SetLoops(-1, LoopType.Restart);
            tween.SetEase(Ease.Linear);
        }

        private void OnDestroy()
        {
            tween?.Kill();
        }
    }
}
