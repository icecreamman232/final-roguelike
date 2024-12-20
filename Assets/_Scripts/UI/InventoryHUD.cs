using System;
using System.Collections;
using System.Collections.Generic;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class InventoryHUD : MonoBehaviour
    {
        [SerializeField] private OpenInventoryUIEvent m_openInventoryUIEvent;
        [SerializeField] private UpdateInventoryUIEvent m_updateInventoryUIEvent;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private InventoryCursor m_inventoryCursor;
        [SerializeField] private EquipmentSlotUI[] m_equipmentSlots;
        [SerializeField] private InventorySlotUI[] m_inventorySlots;
        
        [Header("Infor Panels")]
        [SerializeField] private WeaponInfoPanel m_weaponInfoPanel;
        [SerializeField] private ItemInfoPanel m_itemInfoPanel;

        [SerializeField] private RectTransform m_weaponPanelPivot;
        [SerializeField] private RectTransform m_helmetPanelPivot;
        [SerializeField] private RectTransform m_accessoriesPanelPivot;
        [SerializeField] private RectTransform m_armorPanelPivot;
        [SerializeField] private RectTransform m_glovesPanelPivot;
        [SerializeField] private RectTransform m_bootsPanelPivot;
        [SerializeField] private RectTransform m_charmPanelPivot;
        [SerializeField] private RectTransform[] m_inventoryPanelPivots;
        
        [Header("Runtime")]
        [SerializeField] [ReadOnly] private EquipmentSlotUI m_hoveredEquipmentSlot;
        [SerializeField] [ReadOnly] private InventorySlotUI m_hoveredInventorySlot;
        [SerializeField] [ReadOnly] private EquipmentSlotUI m_selectedEquipmentSlot;
        [SerializeField] [ReadOnly] private InventorySlotUI m_selectedInventorySlot;
        
        private PlayerInventory m_playerInventory;
        
        private void Awake()
        {
            Hide();
            m_openInventoryUIEvent.AddListener(OnInventoryButtonPressed);
            m_updateInventoryUIEvent.AddListener(OnUpdateInventoryUI);
            for (int i = 0; i < m_equipmentSlots.Length; i++)
            {
                m_equipmentSlots[i].PointerEnterAction += OnPointerEnterEquipmentSlot;
                m_equipmentSlots[i].PointerExitAction += OnPointerExitEquipmentSlot;
                m_equipmentSlots[i].PointerDownAction += OnPointerDownEquipmentSlot;
                m_equipmentSlots[i].PointerUpAction += OnPointerUpEquipmentSlot;
            }
            
            for (int i = 0; i < m_inventorySlots.Length; i++)
            {
                m_inventorySlots[i].PointerEnterAction += OnPointerEnterInventorySlot;
                m_inventorySlots[i].PointerExitAction += OnPointerExitInventorySlot;
                m_inventorySlots[i].PointerDownAction += OnPointerDownInventorySlot;
                m_inventorySlots[i].PointerUpAction += OnPointerUpInventorySlot;
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => LevelManager.Instance.PlayerRef != null);
            m_playerInventory = LevelManager.Instance.PlayerRef.GetComponent<PlayerInventory>();
        }

        private void OnDestroy()
        {
            m_updateInventoryUIEvent.RemoveListener(OnUpdateInventoryUI);
            m_openInventoryUIEvent.RemoveListener(OnInventoryButtonPressed);
            
            for (int i = 0; i < m_equipmentSlots.Length; i++)
            {
                m_equipmentSlots[i].PointerEnterAction -= OnPointerEnterEquipmentSlot;
                m_equipmentSlots[i].PointerExitAction -= OnPointerExitEquipmentSlot;
                m_equipmentSlots[i].PointerDownAction -= OnPointerDownEquipmentSlot;
                m_equipmentSlots[i].PointerUpAction -= OnPointerUpEquipmentSlot;
            }
            
            for (int i = 0; i < m_inventorySlots.Length; i++)
            {
                m_inventorySlots[i].PointerEnterAction -= OnPointerEnterInventorySlot;
                m_inventorySlots[i].PointerExitAction -= OnPointerExitInventorySlot;
                m_inventorySlots[i].PointerDownAction -= OnPointerDownInventorySlot;
                m_inventorySlots[i].PointerUpAction -= OnPointerUpInventorySlot;
            }
        }

        private void Show()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
        }
        
        private void UpdateInventoryUI(WeaponData weapon, HelmetData helmet, ArmorData armor, 
            GlovesData gloves, BootsData boots, AccessoriesData accessories, CharmData charm, List<ItemData> inventorySlots)
        {
            //TODO:Check this order
            m_equipmentSlots[0].SetIcon(weapon != null ? weapon.Icon : null);
            m_equipmentSlots[1].SetIcon(helmet != null ? helmet.Icon : null);
            m_equipmentSlots[2].SetIcon(armor != null ? armor.Icon : null);
            m_equipmentSlots[3].SetIcon(accessories != null ? accessories.Icon : null);
            m_equipmentSlots[4].SetIcon(gloves != null ? gloves.Icon : null);
            m_equipmentSlots[5].SetIcon(boots != null ? boots.Icon : null);
            m_equipmentSlots[6].SetIcon(charm != null ? charm.Icon : null);

            for (int i = 0; i < m_inventorySlots.Length; i++)
            {
                m_inventorySlots[i].SetIcon(inventorySlots[i] != null ? inventorySlots[i].Icon : null);
            }
        }
        
        private void OnInventoryButtonPressed(bool isOpen, WeaponData weapon, HelmetData helmet, ArmorData armor, 
            GlovesData gloves, BootsData boots, AccessoriesData accessories, CharmData charm, List<ItemData> inventorySlots)
        {
            if (isOpen)
            {
                Show();
                UpdateInventoryUI(weapon, helmet, armor, gloves, boots, accessories,charm, inventorySlots);
            }
            else
            {
                Hide();
            }
        }
        
        private void OnPointerEnterEquipmentSlot(EquipmentSlotUI equipment)
        {
            m_hoveredEquipmentSlot = equipment;
            
            if (m_selectedEquipmentSlot != null || m_selectedInventorySlot != null) return;
            if (equipment == null) return;
            
            ShowEquipmentInfoPanel(m_playerInventory.GetItemAtEquipment(equipment.ItemCategory));
        }
        
        private void OnPointerExitEquipmentSlot(EquipmentSlotUI equipment)
        {
            if (m_hoveredEquipmentSlot == equipment)
            {
                m_hoveredEquipmentSlot = null;
            }

            HideEquipmentInfoPanel();
        }
        
        private void OnPointerDownEquipmentSlot(EquipmentSlotUI equipment)
        {
           m_selectedEquipmentSlot = equipment;
           m_inventoryCursor.SetIcon(equipment.Icon);
        }
        
        private void OnPointerEnterInventorySlot(InventorySlotUI inventory, int index)
        {
            m_hoveredInventorySlot = inventory;

            if(m_selectedInventorySlot != null || m_selectedEquipmentSlot != null) return;
            if(inventory == null) return;

            ShowInventoryInfoPanel(m_playerInventory.GetItemAtInventorySlot(index), index);
        }
        
        private void OnPointerExitInventorySlot(InventorySlotUI inventory,int index)
        {
            if (m_hoveredInventorySlot == inventory)
            {
                m_hoveredInventorySlot = null;
            }

            HideEquipmentInfoPanel();
        }
        
        private void OnPointerDownInventorySlot(InventorySlotUI inventory, int index)
        {
           m_selectedInventorySlot = inventory;
           m_inventoryCursor.SetIcon(inventory.Icon);
        }
        
        private void OnPointerUpEquipmentSlot(EquipmentSlotUI equipment)
        {
            if (m_hoveredEquipmentSlot == null && m_hoveredInventorySlot == null)
            {
                DropItemOnTheGround();
                return;
            }
            
            if (m_hoveredEquipmentSlot != null)
            {
                //Do nothing since we dont allow switch between equipments
            }
            else if (m_hoveredInventorySlot != null)
            {
                m_playerInventory.MoveEquipmentToInventorySlot(equipment.ItemCategory,
                    m_hoveredInventorySlot.SlotIndex);
            }

            m_inventoryCursor.SetIcon(null);
            m_selectedEquipmentSlot = null;
            m_selectedInventorySlot = null;
        }
        
        private void OnPointerUpInventorySlot(InventorySlotUI inventory, int index)
        {
            if (m_hoveredEquipmentSlot == null && m_hoveredInventorySlot == null)
            {
                DropItemOnTheGround();
                return;
            }
            
            if (m_hoveredEquipmentSlot != null)
            {
                m_playerInventory.MoveInventoryToEquipmentSlot(m_hoveredEquipmentSlot.ItemCategory, index);
            }
            else if (m_hoveredInventorySlot != null)
            {
                m_playerInventory.MoveInventoryToInventorySlot(index,m_hoveredInventorySlot.SlotIndex);
            }
            
            m_inventoryCursor.SetIcon(null);
            m_selectedEquipmentSlot = null;
            m_selectedInventorySlot = null;
        }
        
        
        
        private void OnUpdateInventoryUI(WeaponData weapon, HelmetData helmet, ArmorData armor, GlovesData gloves, 
            BootsData boots, AccessoriesData accessories, CharmData charm, List<ItemData> inventory)
        {
            UpdateInventoryUI(weapon, helmet, armor, gloves, boots, accessories,charm, inventory);
        }

        private void DropItemOnTheGround()
        {
            if (m_selectedEquipmentSlot != null)
            {
                m_playerInventory.DropEquipmentOnTheGround(m_selectedEquipmentSlot.ItemCategory);
                m_inventoryCursor.SetIcon(null);
            }
            else if(m_selectedInventorySlot != null)
            {
                m_playerInventory.DropInventoryItemOnTheGround(m_selectedInventorySlot.SlotIndex);
                m_inventoryCursor.SetIcon(null);
            }
        }

        private void HideEquipmentInfoPanel()
        {
            m_weaponInfoPanel.Hide();
            m_itemInfoPanel.Hide();
        }

        private void ShowEquipmentInfoPanel(ItemData item)
        {
            if (item == null) return;
            switch (item.ItemCategory)
            {
                case ItemCategory.Weapon:
                    ((RectTransform)m_weaponInfoPanel.transform).anchoredPosition = m_weaponPanelPivot.anchoredPosition;
                    m_weaponInfoPanel.Show((WeaponData)item);
                    break;
                case ItemCategory.Helmet:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_helmetPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
                case ItemCategory.Armor:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_armorPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
                case ItemCategory.Boots:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_bootsPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
                case ItemCategory.Gloves:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_glovesPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
                case ItemCategory.Accessories:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_accessoriesPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
                case ItemCategory.Charm:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_charmPanelPivot.anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
            }
        }

        private void ShowInventoryInfoPanel(ItemData item, int slotIndex)
        {
            if (item == null) return;
            switch (item.ItemCategory)
            {
                case ItemCategory.Weapon:
                    ((RectTransform)m_weaponInfoPanel.transform).anchoredPosition = m_inventoryPanelPivots[slotIndex].anchoredPosition;
                    m_weaponInfoPanel.Show((WeaponData)item);
                    break;
                case ItemCategory.Helmet:
                case ItemCategory.Armor:
                case ItemCategory.Boots:
                case ItemCategory.Gloves:
                case ItemCategory.Accessories:
                case ItemCategory.Charm:
                    ((RectTransform)m_itemInfoPanel.transform).anchoredPosition = m_inventoryPanelPivots[slotIndex].anchoredPosition;
                    m_itemInfoPanel.Show(item);
                    break;
            }
        }
    }
}
