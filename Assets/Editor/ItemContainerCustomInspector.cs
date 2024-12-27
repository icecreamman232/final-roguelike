
#if UNITY_EDITOR
using SGGames.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace SGGames.Scripts.EditorExtension
{
    [CustomEditor(typeof(ItemContainer))]
    public class ItemContainerCustomInspector : Editor
    {
        private ItemContainer m_itemContainer;

        private void OnEnable()
        {
            m_itemContainer = target as ItemContainer;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space(30);
            
            if (GUILayout.Button("Add All Items",GUILayout.Height(50)))
            {
                m_itemContainer.ClearData();
                
                var allItemsGUID = AssetDatabase.FindAssets("t:ItemData");
                foreach (var guid in allItemsGUID)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(path);
                    if(itemData.ItemCategory != m_itemContainer.ContainerCategory) continue;
                    m_itemContainer.AddItemToContainer(itemData.Rarity,itemData);
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}

#endif
