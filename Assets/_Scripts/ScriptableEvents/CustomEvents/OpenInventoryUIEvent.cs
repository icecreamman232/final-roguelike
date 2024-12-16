using System;
using System.Collections.Generic;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Open Inventory UI Event")]
    public class OpenInventoryUIEvent : ScriptableObject
    {
        protected Action<bool,WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> m_listeners;
        
        public void AddListener(Action<bool,WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<bool,WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(bool isOpen,WeaponData weapon, HelmetData helmet, ArmorData armor,
            GlovesData gloves, BootsData boots, AccessoriesData accessories,
            CharmData charm,List<ItemData> inventorySlot)
        {
            m_listeners?.Invoke(isOpen,weapon,helmet,armor,gloves,boots,accessories,charm,inventorySlot);
        }
    } 
}