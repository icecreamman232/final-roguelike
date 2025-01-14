using System;
using SGGames.Scripts.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class AbilityCardUI : Button
    {
        [SerializeField] private SelectableAbility m_abilityID;
        [SerializeField] private Image m_abilityIcon;
        [SerializeField] private TextMeshProUGUI m_abilityName;
        [SerializeField] private TextMeshProUGUI m_abilityDesc;

        public Action<SelectableAbility> OnClickCard;

        public void FillInfo(SelectableAbility SelectableAbility, Sprite icon, string name, string desc)
        {
            m_abilityID = SelectableAbility;
            m_abilityIcon.sprite = icon;
            m_abilityName.text = name;
            m_abilityDesc.text = desc;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            OnClickCard?.Invoke(m_abilityID);
        }
    }
}

