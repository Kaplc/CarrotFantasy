using System;
using System.Collections.Generic;
using App.Data.DataClass.Map;
using App.Game.Generic.Map;
using App.Static.Enum;
using UnityEditor;
using UnityEngine;

namespace MapEditorScene
{
    public class EditSceneMap : MonoBehaviour
    {
        private bool isPlaying;
        
        public List<ETowerType> allowBuiltTypeList;

        // 给出Editor调用的Action
        public Action onUpdateAction;
        public Action onDrawGizmosSelectedAction;

        private void Update()
        {
            onUpdateAction?.Invoke();
        }
        
        private void OnDrawGizmosSelected()
        {
            onDrawGizmosSelectedAction?.Invoke();
        }
    }
}