using System;
using System.Collections.Generic;
using SGGames.Scripts.Common;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Data/Item Container")]
    public class ItemContainer : ScriptableObject
    {
        [SerializeField] private ItemCategory m_containerCategory;
        [SerializeField] private List<ItemData> m_commonItemList;
        [SerializeField] private List<ItemData> m_uncommonItemList;
        [SerializeField] private List<ItemData> m_rareItemList;
        [SerializeField] private List<ItemData> m_epicItemList;
        [SerializeField] private List<ItemData> m_legendaryItemList;

        public ItemCategory ContainerCategory => m_containerCategory;
        
        public GameObject GetCommonItem()
        {
            return m_commonItemList[RandomController.GetRandomIntInRange(0, m_commonItemList.Count)].PickerPrefab;
        }
        public GameObject GetUncommonItem()
        {
            return m_uncommonItemList[RandomController.GetRandomIntInRange(0, m_uncommonItemList.Count)].PickerPrefab;
        }
        public GameObject GetRareItem()
        {
            return m_rareItemList[RandomController.GetRandomIntInRange(0, m_rareItemList.Count)].PickerPrefab;
        }
        public GameObject GetEpicItem()
        {
            return m_epicItemList[RandomController.GetRandomIntInRange(0, m_epicItemList.Count)].PickerPrefab;
        }
        public GameObject GetLegendaryItem()
        {
            return m_legendaryItemList[RandomController.GetRandomIntInRange(0, m_legendaryItemList.Count)].PickerPrefab;
        }
        
        #if UNITY_EDITOR
        public void AddItemToContainer(Rarity rarity, ItemData item)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    m_commonItemList.Add(item);
                    break;
                case Rarity.Uncommon:
                    m_uncommonItemList.Add(item);
                    break;
                case Rarity.Rare:
                    m_rareItemList.Add(item);
                    break;
                case Rarity.Epic:
                    m_epicItemList.Add(item);
                    break;
                case Rarity.Legendary:
                    m_legendaryItemList.Add(item);
                    break;
            }
        }
        #endif
    }
}

