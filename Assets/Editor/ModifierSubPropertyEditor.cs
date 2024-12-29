#if UNITY_EDITOR
using SGGames.Scripts.Common;
using SGGames.Scripts.Modifiers;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    
    public class MovementModifierSubEditor
    {
        private MovementModifierType m_movementModifierType;
        private float m_modifierValue;
    
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Movement Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_movementModifierType = (MovementModifierType)EditorGUILayout.EnumPopup("Movement Modifier Type",m_movementModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<MovementModifier>();
            asset.MovementModifierType = m_movementModifierType;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class HealthModifierSubEditor
    {
        private HealthModifierType m_healthModifierType;
        private float m_modifierValue;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Health Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_healthModifierType = (HealthModifierType)EditorGUILayout.EnumPopup("Health Modifier Type",m_healthModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<HealthModifier>();
            asset.HealthModifierType = m_healthModifierType;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class DamageModifierSubEditor
    {
        private DamageModifierType m_damageModifierType;
        private float m_modifierValue;
        private float m_chanceToCause;

        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Damage Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_damageModifierType = (DamageModifierType)EditorGUILayout.EnumPopup("Damage Modifier Type",m_damageModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
            m_chanceToCause = EditorGUILayout.FloatField("Chance To Cause", m_chanceToCause);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<DamageModifier>();
            asset.DamageModifierType = m_damageModifierType;
            asset.ModifierValue = m_modifierValue;
            asset.ChanceToCause = m_chanceToCause;
            return asset;
        }
    }

    public class GameEventModifierSubEditor
    {
        private GameEventType m_gameEventToTrigger;
        private Modifier m_modifierToTrigger;
        private bool m_triggerOnce;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("GameEvent Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_gameEventToTrigger = (GameEventType)EditorGUILayout.EnumPopup("GameEvent Type",m_gameEventToTrigger);
            m_modifierToTrigger = EditorGUILayout.ObjectField("Modifier To Trigger",m_modifierToTrigger,typeof(Modifier),allowSceneObjects:false) as Modifier;
            m_triggerOnce = EditorGUILayout.Toggle("Trigger Once", m_triggerOnce);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<GameEventModifier>();
            asset.EventTypeToTrigger = m_gameEventToTrigger;
            asset.ModifierToBeTriggered = m_modifierToTrigger;
            asset.TriggerOnce = m_triggerOnce;
            return asset;
        }
    }

    public class ArmorModifierSubEditor
    {
        private ArmorModifierType m_armorModifierType;
        private float m_modifierValue;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Armor Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_armorModifierType = (ArmorModifierType)EditorGUILayout.EnumPopup("Armor Modifier Type",m_armorModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<ArmorModifier>();
            asset.ArmorModifierType = m_armorModifierType;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class CoinModifierSubEditor
    {
        private CoinModifierType m_coinModifierType;
        private int m_modifierValue;

        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Coin Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_coinModifierType = (CoinModifierType)EditorGUILayout.EnumPopup("Coin Modifier Type",m_coinModifierType);
            m_modifierValue = EditorGUILayout.IntField("Modifier Value", m_modifierValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<CoinModifier>();
            asset.CoinModifierType = m_coinModifierType;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class PlayerEventModifierSubEditor
    {
        private PlayerEventType m_playerEventType;
        private Modifier m_modifierToTrigger;
        private bool m_triggerOnce;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Player Event Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_playerEventType = (PlayerEventType)EditorGUILayout.EnumPopup("Player Event Type",m_playerEventType);
            m_modifierToTrigger = EditorGUILayout.ObjectField("Modifier To Trigger",m_modifierToTrigger,typeof(Modifier),allowSceneObjects:false) as Modifier;
            m_triggerOnce = EditorGUILayout.Toggle("Trigger Once", m_triggerOnce);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<PlayerEventModifier>();
            asset.EventTypeToTrigger = m_playerEventType;
            asset.ModifierToBeTriggered = m_modifierToTrigger;
            asset.TriggerOnce = m_triggerOnce;
            return asset;
        }
    }

    public class HealingModifierSubEditor
    {
        private HealingModifierType m_healingModifierType;
        private float m_modifierValue;
        private float m_chanceToHeal;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Healing Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_healingModifierType = (HealingModifierType)EditorGUILayout.EnumPopup("Healing Modifier Type",m_healingModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
            m_chanceToHeal = EditorGUILayout.FloatField("Chance To Heal", m_chanceToHeal);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<HealingModifier>();
            asset.HealingModifierType = m_healingModifierType;
            asset.ChanceToHeal = m_chanceToHeal;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class AttributeModifierSubEditor
    {
        private AttributeType m_attributeType;
        private int m_modifierValue;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Attribute Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_attributeType = (AttributeType)EditorGUILayout.EnumPopup("Attribute Type",m_attributeType);
            m_modifierValue = EditorGUILayout.IntField("Modifier Value", m_modifierValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<AttributeModifier>();
            asset.AttributeType = m_attributeType;
            asset.ModifierValue = m_modifierValue;
            return asset;
        }
    }

    public class ManaModifierSubEditor
    {
        private ManaModifierType m_manaModifierType;
        private float m_modifierValue;
        private bool m_isPercentValue;
        
        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Mana Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_manaModifierType = (ManaModifierType)EditorGUILayout.EnumPopup("Mana Modifier Type",m_manaModifierType);
            m_modifierValue = EditorGUILayout.FloatField("Modifier Value", m_modifierValue);
            m_isPercentValue = EditorGUILayout.Toggle("Is Percent", m_isPercentValue);
        }
        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<ManaModifier>();
            asset.ManaModifierType = m_manaModifierType;
            asset.ModifierValue = m_modifierValue;
            asset.IsPercentValue = m_isPercentValue;
            return asset;
        }
    }

    public class ConvertManaToDmgModifierSubEditor
    {
        private float m_manaToDmgRate;

        public void DrawSubEditor(GUIStyle style)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Mana To Damage Modifier");
            EditorGUILayout.Space(10);
            
            style.alignment = TextAnchor.MiddleLeft;
            m_manaToDmgRate = EditorGUILayout.FloatField("Convert Rate", m_manaToDmgRate);
        }

        public Modifier CreateModifier()
        {
            var asset = ScriptableObject.CreateInstance<ConvertManaToDamageModifier>();
            asset.ManaToDamageRate = m_manaToDmgRate;
            return asset;
        }
    }
}

#endif
