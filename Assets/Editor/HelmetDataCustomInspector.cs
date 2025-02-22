
#if UNITY_EDITOR
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtensions
{
    [CustomEditor(typeof(HelmetData))]
    public class HelmetDataCustomInspector : Editor
    {
        private HelmetData m_data;

        private void OnEnable()
        {
            if (m_data == null)
            {
                m_data = (HelmetData)target;
            }
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Label(AssetPreview.GetAssetPreview(m_data.Icon));
            base.OnInspectorGUI();
        }
    }
}

#endif
