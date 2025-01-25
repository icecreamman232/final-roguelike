using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtensions
{
    [CustomPropertyDrawer(typeof(GroupBooleanAttribute))]
    public class GroupBooleanAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw the current property
            EditorGUI.BeginChangeCheck();
            bool currentValue = EditorGUI.Toggle(position, label, property.boolValue);
            if (EditorGUI.EndChangeCheck())
            {
                // If we set the current property to true, set others in the group to false
                property.boolValue = currentValue;
                var iterator = property.serializedObject.GetIterator();
                if (currentValue)
                {
                    do
                    {
                        if (iterator.name != property.name && iterator.propertyType == SerializedPropertyType.Boolean)
                        {
                            iterator.boolValue = false;
                        }
                    } 
                    while (iterator.NextVisible(true));
                }

                property.serializedObject.ApplyModifiedProperties();
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
