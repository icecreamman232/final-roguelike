using System;
using System.Collections.Generic;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Modifiers;
using SGGames.Scripts.Weapons;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerInventory : PlayerBehavior
    {
        [Header("Refs")] 
        [SerializeField] private PlayerWeaponHandler m_playerWeaponHandler;
        [SerializeField] private ModifierHandler m_playerModifierHandler;
        [Header("Equipment Slots")]
        [SerializeField] private WeaponData m_primaryWeaponSlot;
        [SerializeField] private HelmetData m_helmetSlot;
        [Header("Events")]
        [SerializeField] private ItemPickedEvent m_itemPickedEvent;
        [Header("Inventory Slots")]
        [SerializeField] private List<ItemData> m_inventorySlots;

        private readonly int C_MAX_INVENTORY_SLOT = 5;
        private int m_occupiedInventoryNumber = 0;
        
        protected override void Start()
        {
            base.Start();
            m_inventorySlots = new List<ItemData>(C_MAX_INVENTORY_SLOT);
            for (int i = 0; i < C_MAX_INVENTORY_SLOT; i++)
            {
                m_inventorySlots.Add(null);
            }
            m_itemPickedEvent.AddListener(OnItemPicked);
        }

        private void OnDestroy()
        {
            m_itemPickedEvent.RemoveListener(OnItemPicked);
        }
        
        private void OnItemPicked(ItemCategory category, ItemData data,GameObject picker)
        {
            switch (category)
            {
                case ItemCategory.Weapon:
                    TryAddWeapon((WeaponData)data, picker);
                    break;
                case ItemCategory.Helmet:
                    TryAddHelmet((HelmetData)data, picker);
                    break;
                case ItemCategory.Armor:
                    break;
                case ItemCategory.Boots:
                    break;
                case ItemCategory.Gloves:
                    break;
                case ItemCategory.Accessories:
                    break;
                case ItemCategory.Charm:
                    break;
            }
        }

        public void AddInitialWeapon(WeaponData data)
        {
            m_primaryWeaponSlot = data;
        }

        private void TryAddWeapon(WeaponData data, GameObject picker)
        {
            //Primary slot is empty => add to slot and equip weapon
            if (m_primaryWeaponSlot == null)
            {
                m_primaryWeaponSlot = data;
                m_playerWeaponHandler.EquipWeapon(data.WeaponPrefab.GetComponent<Weapon>());
                Destroy(picker);
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    Destroy(picker);
                }
            }
        }

        private void TryAddHelmet(HelmetData data, GameObject picker)
        {
            if (m_helmetSlot == null)
            {
                m_helmetSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }
                Destroy(picker);
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    Destroy(picker);
                }
            }
        }

        private bool AddToEmptyInventorySlot(ItemData data)
        {
            //Has no empty slot
            if (m_occupiedInventoryNumber >= C_MAX_INVENTORY_SLOT) return false;

            for (int i = 0; i < C_MAX_INVENTORY_SLOT; i++)
            {
                if (m_inventorySlots[i] == null)
                {
                    m_inventorySlots[i] = data;
                    return true;
                }
            }

            return false;   
        }
    }
}

