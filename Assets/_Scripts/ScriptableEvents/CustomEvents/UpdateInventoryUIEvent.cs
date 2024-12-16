using System;
using System.Collections.Generic;
using SGGames.Scripts.Data;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(menuName = "SGGames/Event/Update Inventory UI Event")]
    public class UpdateInventoryUIEvent : ScriptableObject
    {
        protected Action<WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> m_listeners;
        
        public void AddListener(Action<WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<WeaponData,HelmetData,ArmorData,
            GlovesData,BootsData,AccessoriesData,
            CharmData,List<ItemData>> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(WeaponData weapon, HelmetData helmet, ArmorData armor,
            GlovesData gloves, BootsData boots, AccessoriesData accessories,
            CharmData charm,List<ItemData> inventorySlot)
        {
            m_listeners?.Invoke(weapon,helmet,armor,gloves,boots,accessories,charm,inventorySlot);
        }
    } 
}