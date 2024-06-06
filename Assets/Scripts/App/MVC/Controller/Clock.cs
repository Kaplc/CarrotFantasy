using System;
using UnityEngine;

namespace App.MVC.Controller
{
    public class Clock: MonoBehaviour
    {
        private bool isPaused = true;
        private float time;

        private void Update()
        {
            if (!isPaused)
            {
                time += Time.deltaTime;
            }
        }

        #region 控制时钟

        public void Pause()
        {
            isPaused = true;
        }
        
        public void Continue()
        {
            isPaused = false;
        }
        
        public void Reset()
        {
            time = 0;
            isPaused = true;
        }

        public void Run()
        {
            isPaused = true;
        }

        #endregion

        #region 获取信息

        public float GetTime()
        {
            return time;
        }

        #endregion
    }
}