
#if UNITY_EDITOR
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    [CustomEditor(typeof(WeaponData))]
    public class WeaponDataCustomInspector : Editor
    {
        private WeaponData m_data;

        private void OnEnable()
        {
            m_data = target as WeaponData;
        }

        public override void OnInspectorGUI()
        {
            var texture = AssetPreview.GetAssetPreview(m_data.Icon);
            GUILayout.Label(texture);
            base.OnInspectorGUI();

            if (GUILayout.Button("Apply Data"))
            {
                m_data.ApplyData();
            }
        }
    }
}

#endif

