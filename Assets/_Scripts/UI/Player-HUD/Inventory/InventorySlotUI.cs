using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class InventorySlotUI : Button
    {
        [SerializeField] private int m_slotIndex;
        [SerializeField] private Image m_icon;

        public Action<InventorySlotUI,int> PointerEnterAction;
        public Action<InventorySlotUI,int> PointerExitAction;
        public Action<InventorySlotUI,int> PointerUpAction;
        public Action<InventorySlotUI,int> PointerDownAction;
        
        public Sprite Icon => m_icon.sprite;
        public int SlotIndex => m_slotIndex;
        
        public void SetIcon(Sprite icon)
        {
            m_icon.gameObject.SetActive(icon != null);
            m_icon.sprite = icon;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            PointerEnterAction.Invoke(this,m_slotIndex);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            PointerExitAction.Invoke(this,m_slotIndex);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            PointerDownAction.Invoke(this,m_slotIndex);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            PointerUpAction.Invoke(this,m_slotIndex);
        }
    }
}
