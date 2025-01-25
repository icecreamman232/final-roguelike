using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtensions
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIfAttribute = attribute as ShowIfAttribute;
            bool shouldDisplay = GetConditionAttributeValue(showIfAttribute, property);
            if (shouldDisplay)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        private bool GetConditionAttributeValue(ShowIfAttribute conditionAttribute, SerializedProperty property)
        {
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, conditionAttribute.ConditionBoolean);
            SerializedProperty sourceProperty = property.serializedObject.FindProperty(conditionPath);

            if (sourceProperty != null)
            {
                return sourceProperty.boolValue;
            }
            
            Debug.LogError($"Property {propertyPath} does not exist on {conditionPath}");
            
            return false;
        }
    }
}


