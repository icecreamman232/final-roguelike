using System;
using System.Collections.Generic;
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Modifiers;
using SGGames.Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private ArmorData m_armorSlot;
        [SerializeField] private GlovesData m_glovesSlot;
        [SerializeField] private BootsData m_bootsSlot;
        [SerializeField] private AccessoriesData m_accessoriesSlot;
        [SerializeField] private CharmData m_charmSlot;
        [Header("Events")] 
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private BoolEvent m_toggleFreezePlayerEvent;
        [SerializeField] private OpenInventoryUIEvent m_openInventoryUI;
        [SerializeField] private UpdateInventoryUIEvent m_updateInventoryUI;
        [SerializeField] private ItemPickedEvent m_itemPickedEvent;
        [Header("Inventory Slots")]
        [SerializeField] private List<ItemData> m_inventorySlots;

        private PlayerInputAction m_playerInputAction;
        private readonly int C_MAX_INVENTORY_SLOT = 6;
        private int m_occupiedInventoryNumber = 0;
        private bool m_isOpeningInventoryUI;
        
        protected override void Start()
        {
            base.Start();
            m_inventorySlots = new List<ItemData>(C_MAX_INVENTORY_SLOT);
            for (int i = 0; i < C_MAX_INVENTORY_SLOT; i++)
            {
                m_inventorySlots.Add(null);
            }
            m_itemPickedEvent.AddListener(OnItemPicked);
            m_playerInputAction = new PlayerInputAction();
            m_playerInputAction.Player.Enable();
            m_playerInputAction.Player.Inventory.performed += OnInventoryButtonPressed;
        }

        private void OnInventoryButtonPressed(InputAction.CallbackContext context)
        {
            m_isOpeningInventoryUI = !m_isOpeningInventoryUI;
            m_toggleFreezePlayerEvent?.Raise(m_isOpeningInventoryUI);
            m_gameEvent.Raise(m_isOpeningInventoryUI ? GameEventType.PAUSED : GameEventType.UNPAUSED);
            m_openInventoryUI?.Raise(m_isOpeningInventoryUI,m_primaryWeaponSlot,m_helmetSlot,m_armorSlot,m_glovesSlot,
                m_bootsSlot,m_accessoriesSlot,m_charmSlot,m_inventorySlots);
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
                    if (TryAddWeapon((WeaponData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Helmet:
                    if (TryAddHelmet((HelmetData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Armor:
                    if (TryAddArmor((ArmorData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Boots:
                    if (TryAddBoots((BootsData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Gloves:
                    if (TryAddGloves((GlovesData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Accessories:
                    if (TryAddAccessories((AccessoriesData)data))
                    {
                        Destroy(picker);
                    }
                    break;
                case ItemCategory.Charm:
                    if (TryAddCharm((CharmData)data))
                    {
                        Destroy(picker);
                    }
                    break;
            }
        }

        public void AddInitialWeapon(WeaponData data)
        {
            m_primaryWeaponSlot = data;
        }

        #region Add Equipment
        private bool TryAddWeapon(WeaponData data)
        {
            //Primary slot is empty => add to slot and equip weapon
            if (m_primaryWeaponSlot == null)
            {
                m_primaryWeaponSlot = data;
                m_playerWeaponHandler.EquipWeapon(data.WeaponPrefab.GetComponent<Weapon>());
                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }

            return false;
        }

        private bool TryAddHelmet(HelmetData data)
        {
            if (m_helmetSlot == null)
            {
                m_helmetSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }

                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool TryAddArmor(ArmorData data)
        {
            if (m_armorSlot == null)
            {
                m_armorSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }

                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true; 
                }
            }
            return false;
        }
        
        private bool TryAddBoots(BootsData data)
        {
            if (m_bootsSlot == null)
            {
                m_bootsSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }

                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }
            return false;
        }

        
        private bool TryAddGloves(GlovesData data)
        {
            if (m_glovesSlot == null)
            {
                m_glovesSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }
                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }
            return false;
        }
        
        private bool TryAddAccessories(AccessoriesData data)
        {
            if (m_accessoriesSlot == null)
            {
                m_accessoriesSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }
                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }
            return false;
        }
        
        private bool TryAddCharm(CharmData data)
        {
            if (m_charmSlot == null)
            {
                m_charmSlot = data;
                foreach (var modifier in data.ModifierList)
                {
                    m_playerModifierHandler.RegisterModifier(modifier);
                }
                return true;
            }
            else
            {
                if (AddToEmptyInventorySlot(data))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        
        #region Remove Equipment

        private void RemoveWeaponEquipment()
        {
            m_playerWeaponHandler.UnEquipWeapon();
            m_primaryWeaponSlot = null;
        }

        private void RemoveHelmetEquipment()
        {
            foreach (var modifier in m_helmetSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_helmetSlot = null;
        }

        private void RemoveArmorEquipment()
        {
            foreach (var modifier in m_armorSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_armorSlot = null;
        }

        private void RemoveAccessoriesEquipment()
        {
            foreach (var modifier in m_accessoriesSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_accessoriesSlot = null;
        }

        private void RemoveGlovesEquipment()
        {
            foreach (var modifier in m_glovesSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_glovesSlot = null;
        }

        private void RemoveBootsEquipment()
        {
            foreach (var modifier in m_bootsSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_bootsSlot = null;
        }

        private void RemoveCharmEquipment()
        {
            foreach (var modifier in m_charmSlot.ModifierList)
            {
                m_playerModifierHandler.UnregisterModifier(modifier);
            }
            m_charmSlot = null;
        }
        #endregion

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

        public ItemData GetItemAtInventorySlot(int index)
        {
            return m_inventorySlots[index];
        }

        private ItemData GetItemAtEquipment(ItemCategory equipmentCategory)
        {
            switch (equipmentCategory)
            {
                case ItemCategory.Weapon:
                    return m_primaryWeaponSlot;
                case ItemCategory.Helmet:
                    return m_helmetSlot;
                case ItemCategory.Armor:
                    return m_armorSlot;
                case ItemCategory.Boots:
                    return m_bootsSlot;
                case ItemCategory.Gloves:
                    return m_glovesSlot;
                case ItemCategory.Accessories:
                    return m_accessoriesSlot;
                case ItemCategory.Charm:
                    return m_charmSlot;
                default:
                    return null;
            }
        }

        private void AddToEquipmentSlot(ItemData item)
        {
            switch (item.ItemCategory)
            {
                case ItemCategory.Weapon:
                    TryAddWeapon((WeaponData)item);
                    break;
                case ItemCategory.Helmet:
                    TryAddHelmet((HelmetData)item);
                    break;
                case ItemCategory.Armor:
                    TryAddArmor((ArmorData)item);
                    break;
                case ItemCategory.Boots:
                    TryAddBoots((BootsData)item);
                    break;
                case ItemCategory.Gloves:
                    TryAddGloves((GlovesData)item) ;
                    break;
                case ItemCategory.Accessories:
                    TryAddAccessories((AccessoriesData)item);
                    break;
                case ItemCategory.Charm:
                    TryAddCharm((CharmData)item);
                    break;
            }
        }

        private void RemoveEquipmentSlot(ItemData item)
        {
            switch (item.ItemCategory)
            {
                case ItemCategory.Weapon:
                    RemoveWeaponEquipment();
                    break;
                case ItemCategory.Helmet:
                    RemoveHelmetEquipment();
                    break;
                case ItemCategory.Armor:
                    RemoveArmorEquipment();
                    break;
                case ItemCategory.Boots:
                    RemoveBootsEquipment();
                    break;
                case ItemCategory.Gloves:
                    RemoveGlovesEquipment();
                    break;
                case ItemCategory.Accessories:
                    RemoveAccessoriesEquipment();
                    break;
                case ItemCategory.Charm:
                    RemoveCharmEquipment();
                    break;
            }
        }

        public void MoveEquipmentToInventorySlot(ItemCategory equipmentCategory, int inventorySlotIndex)
        {
            if (m_inventorySlots[inventorySlotIndex] == null)
            {
                m_inventorySlots[inventorySlotIndex] = GetItemAtEquipment(equipmentCategory);
                switch (equipmentCategory)
                {
                    case ItemCategory.Weapon:
                        m_primaryWeaponSlot = null;
                        m_playerWeaponHandler.UnEquipWeapon();
                        break;
                    case ItemCategory.Helmet:
                        foreach (var modifier in m_helmetSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_helmetSlot = null;
                        break;
                    case ItemCategory.Armor:
                        foreach (var modifier in m_armorSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_armorSlot = null;
                        break;
                    case ItemCategory.Boots:
                        foreach (var modifier in m_bootsSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_bootsSlot = null;
                        break;
                    case ItemCategory.Gloves:
                        foreach (var modifier in m_glovesSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_glovesSlot = null;
                        break;
                    case ItemCategory.Accessories:
                        foreach (var modifier in m_accessoriesSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_accessoriesSlot = null;
                        break;
                    case ItemCategory.Charm:
                        foreach (var modifier in m_charmSlot.ModifierList)
                        {
                            m_playerModifierHandler.UnregisterModifier(modifier);
                        }
                        m_charmSlot = null;
                        break;
                }
            }
            else
            {
                var itemDataAtInventorySlot = m_inventorySlots[inventorySlotIndex];
                var itemAtEquipmentSlot = GetItemAtEquipment(equipmentCategory);
                
                if (itemAtEquipmentSlot != null)
                {
                    RemoveEquipmentSlot(itemAtEquipmentSlot);
                }
                AddToEquipmentSlot(itemDataAtInventorySlot);
                m_inventorySlots[inventorySlotIndex] = itemAtEquipmentSlot;
            }
            m_updateInventoryUI.Raise(m_primaryWeaponSlot,m_helmetSlot,m_armorSlot,m_glovesSlot,m_bootsSlot,m_accessoriesSlot,m_charmSlot,m_inventorySlots);
        }

        public void MoveInventoryToEquipmentSlot(ItemCategory equipmentCategory, int inventorySlotIndex)
        {
            if (m_inventorySlots[inventorySlotIndex].ItemCategory != equipmentCategory) return;

            var itemAtInventorySlot = m_inventorySlots[inventorySlotIndex];
            var itemAtEquipmentSlot = GetItemAtEquipment(equipmentCategory);
            
            if (itemAtEquipmentSlot != null)
            {
                RemoveEquipmentSlot(itemAtEquipmentSlot);
            }
            m_inventorySlots[inventorySlotIndex] = itemAtEquipmentSlot;
            AddToEquipmentSlot(itemAtInventorySlot);
            
            m_updateInventoryUI.Raise(m_primaryWeaponSlot,m_helmetSlot,m_armorSlot,m_glovesSlot,m_bootsSlot,m_accessoriesSlot,m_charmSlot,m_inventorySlots);
        }

        public void MoveInventoryToInventorySlot(int from, int to)
        {
            var itemAtInventoryFrom = m_inventorySlots[from];
            var itemAtInventoryTo = m_inventorySlots[to];
            m_inventorySlots[from] = itemAtInventoryTo;
            m_inventorySlots[to] = itemAtInventoryFrom;
            
            m_updateInventoryUI.Raise(m_primaryWeaponSlot,m_helmetSlot,m_armorSlot,m_glovesSlot,m_bootsSlot,m_accessoriesSlot,m_charmSlot,m_inventorySlots);
        }
    }
}

