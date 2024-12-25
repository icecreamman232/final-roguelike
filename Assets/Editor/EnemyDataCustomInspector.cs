
#if UNITY_EDITOR
using System;
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    [CustomEditor(typeof(EnemyData))]
    public class EnemyDataCustomInspector : Editor
    {
        private EnemyData m_data;


        private void OnEnable()
        {
            m_data = target as EnemyData;
        }

        public override void OnInspectorGUI()
        {
            var texture = AssetPreview.GetAssetPreview(m_data.Sprite);
            GUILayout.Label(texture);
            base.OnInspectorGUI();
        }
    }
}
#endif

