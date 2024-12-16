using System;
using SGGames.Scripts.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class EquipmentSlotUI : Button
    {
        [SerializeField] private ItemCategory m_itemCategory;
        [SerializeField] private Image m_icon;
        [SerializeField] private GameObject m_emptyOverlay;
        
        public Action<EquipmentSlotUI> PointerEnterAction;
        public Action<EquipmentSlotUI> PointerExitAction;
        public Action<EquipmentSlotUI> PointerDownAction;
        public Action<EquipmentSlotUI> PointerUpAction;
        
        public ItemCategory ItemCategory => m_itemCategory;
        
        public Sprite Icon => m_icon.sprite;
        
        public void SetIcon(Sprite icon)
        {
            m_icon.sprite = icon;
            m_icon.gameObject.SetActive(icon != null);
            m_emptyOverlay.SetActive(icon == null);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            PointerEnterAction.Invoke(this);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            PointerExitAction.Invoke(this);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            PointerDownAction.Invoke(this);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            PointerUpAction.Invoke(this);
        }
    }
}

