
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Data;
using SGGames.Scripts.EditorExtension;
using SGGames.Scripts.Modifiers;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    public class CreateModifierEditorWindow : EditorWindow
    {
        private readonly string C_MODIFIER_PATH = "Assets/_Data/Modifier/"; 
        
        private string m_modifierName;
        private ItemData m_itemData;
        private GUIStyle m_windowStyleCache;
        private GUIStyle m_textAreaStyleCache;
        private ModifierType m_modifierType;
        private float m_duration;
        private bool m_isInstantTrigger;
        private string m_modifierDesc;
        private PostTriggerModifierBehavior m_postTriggerBehavior;
        
        //Sub editor
        private MovementModifierSubEditor m_movementSubEditor;
        private HealthModifierSubEditor m_healthSubEditor;
        private DamageModifierSubEditor m_damageSubEditor;
        private GameEventModifierSubEditor m_gameEventSubEditor;
        private ArmorModifierSubEditor m_armorSubEditor;
        private CoinModifierSubEditor m_coinSubEditor;
        private PlayerEventModifierSubEditor m_playerEventSubEditor;
        private HealingModifierSubEditor m_healingSubEditor;
        private AttributeModifierSubEditor m_attributeModifierSubEditor;
        private ManaModifierSubEditor m_manaModifierSubEditor;
        private ConvertManaToDmgModifierSubEditor m_convertManaToDmgModifierSubEditor;
        
        [MenuItem("SGGames/Create Modifier Tool")]
        private static void OpenWindow()
        {
            var window = EditorWindow.GetWindow<CreateModifierEditorWindow>("Modifier Tool");
            window.ShowPopup();
        }
    
        private void OnEnable()
        {
            m_movementSubEditor          ??= new MovementModifierSubEditor();
            m_healthSubEditor            ??= new HealthModifierSubEditor();
            m_damageSubEditor            ??= new DamageModifierSubEditor();
            m_gameEventSubEditor         ??= new GameEventModifierSubEditor();
            m_armorSubEditor             ??= new ArmorModifierSubEditor();
            m_coinSubEditor              ??=new CoinModifierSubEditor();
            m_playerEventSubEditor       ??= new PlayerEventModifierSubEditor();
            m_healingSubEditor           ??= new HealingModifierSubEditor();
            m_attributeModifierSubEditor ??= new AttributeModifierSubEditor();
            m_manaModifierSubEditor      ??= new ManaModifierSubEditor();
            m_convertManaToDmgModifierSubEditor ??= new ConvertManaToDmgModifierSubEditor();
        }
    
        private void OnGUI()
        {
            m_windowStyleCache = EditorStyles.label;
            m_windowStyleCache.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Data");
            m_windowStyleCache.alignment = TextAnchor.MiddleLeft;
            
            m_modifierName = EditorGUILayout.TextField("Modifier Name", m_modifierName);
            m_itemData = EditorGUILayout.ObjectField("Item Data", m_itemData, typeof(ItemData), false) as ItemData;
            m_modifierType = (ModifierType)EditorGUILayout.EnumPopup("Modifier Type",m_modifierType);
            m_duration = EditorGUILayout.FloatField("Duration", m_duration);
            m_isInstantTrigger = EditorGUILayout.Toggle("Instant Trigger", m_isInstantTrigger);
            m_postTriggerBehavior = (PostTriggerModifierBehavior)EditorGUILayout.EnumPopup("Post Trigger Behavior",m_postTriggerBehavior);
            EditorGUILayout.PrefixLabel("Description");
            m_modifierDesc = EditorGUILayout.TextArea(m_modifierDesc,GUILayout.MinHeight(50));
            EditorGUILayout.Space(10);
            
            m_textAreaStyleCache = EditorStyles.textArea;
            m_textAreaStyleCache.richText = true;
            EditorGUILayout.PrefixLabel("Review Description");
            EditorGUILayout.TextArea(m_modifierDesc,m_textAreaStyleCache,GUILayout.MinHeight(50));
            switch (m_modifierType)
            {
                case ModifierType.MOVEMENT:
                    m_movementSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.HEALTH:
                    m_healthSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.DAMAGE:
                    m_damageSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.GAME_EVENT:
                    m_gameEventSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.ARMOR:
                    m_armorSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.COIN:
                    m_coinSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.PLAYER_EVENT:
                    m_playerEventSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.HEALING:
                    m_healingSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.ATTRIBUTE:
                    m_attributeModifierSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.MANA:
                    m_manaModifierSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
                case ModifierType.CONVERT_MANA_TO_DAMAGE:
                    m_convertManaToDmgModifierSubEditor.DrawSubEditor(m_windowStyleCache);
                    break;
            }

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Create Modifier And Add To Item",GUILayout.Height(30)))
            {
                AddModifier();
            }
        }

        private void SaveCommonData(Modifier modifier)
        {
            modifier.ModifierType = m_modifierType;
            modifier.Description = m_modifierDesc;
            modifier.Duration = m_duration;
            modifier.InstantTrigger = m_isInstantTrigger;
            modifier.AfterStopBehavior = m_postTriggerBehavior;
        }

        private void AddModifier()
        {
            Modifier modifier = null;
            switch (m_modifierType)
            {
                case ModifierType.MOVEMENT:
                    modifier = m_movementSubEditor.CreateModifier();
                    break;
                case ModifierType.HEALTH:
                    modifier = m_healthSubEditor.CreateModifier();
                    break;
                case ModifierType.DAMAGE:
                    modifier = m_damageSubEditor.CreateModifier();
                    break;
                case ModifierType.GAME_EVENT:
                    modifier = m_gameEventSubEditor.CreateModifier();
                    break;
                case ModifierType.ARMOR:
                    modifier = m_armorSubEditor.CreateModifier();
                    break;
                case ModifierType.COIN:
                    modifier = m_coinSubEditor.CreateModifier();
                    break;
                case ModifierType.PLAYER_EVENT:
                    modifier = m_playerEventSubEditor.CreateModifier();
                    break;
                case ModifierType.HEALING:
                    modifier = m_healingSubEditor.CreateModifier();
                    break;
                case ModifierType.ATTRIBUTE:
                    modifier = m_attributeModifierSubEditor.CreateModifier();
                    break;
                case ModifierType.MANA:
                    modifier = m_manaModifierSubEditor.CreateModifier();
                    break;
                case ModifierType.CONVERT_MANA_TO_DAMAGE:
                    modifier = m_convertManaToDmgModifierSubEditor.CreateModifier();
                    break;
            }

            SaveCommonData(modifier);
            
            AssetDatabase.CreateAsset(modifier,C_MODIFIER_PATH + m_modifierName + ".asset");
            var modifierList = m_itemData.ModifierList.ToList();
            modifierList.Add(modifier);
            m_itemData.ModifierList = modifierList.ToArray();
            
            EditorUtility.SetDirty(m_itemData);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = modifier;
        }
    }
}
#endif
