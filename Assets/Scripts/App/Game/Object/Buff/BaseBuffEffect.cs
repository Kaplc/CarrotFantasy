using GameFramework;
using UnityEngine;

namespace App.Game.Generic.BaseObject
{
    public class BaseBuffEffect : MonoBehaviour, IPoolObject
    {
        public virtual void OnGet()
        {
        }

        public virtual void OnPush()
        {
        }
    }
}