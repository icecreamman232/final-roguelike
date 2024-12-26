using System;
using SGGames.Scripts.Modifiers;
using UnityEditor;
using UnityEngine;


namespace SGGames.Scripts.EditorExtension
{
    [CustomEditor(typeof(Modifier),true)]
    public class ModifierCustomInspector : Editor
    {
        private Modifier m_modifier;
        private GUIStyle m_textAreaStyles;
        private bool m_isInitStyle;

        private void OnEnable()
        {
            m_modifier = target as Modifier;
            
        }

        private void InitStyle()
        {
            m_textAreaStyles = new GUIStyle(EditorStyles.textArea)
            {
                richText = true
            };
            m_isInitStyle = true;
        }

        public override void OnInspectorGUI()
        {
            if (!m_isInitStyle)
            {
                InitStyle();
            }
            
            EditorGUILayout.LabelField("Description In-game");
            EditorGUILayout.TextArea(m_modifier.Description,m_textAreaStyles);
            EditorGUILayout.Space(80);
            base.OnInspectorGUI();
        }
    }
}

