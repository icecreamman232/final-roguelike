using System;
using SGGames.Scripts.Data;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WeaponPickerHUD : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarity;
        [SerializeField] private TextMeshProUGUI m_category;
        [SerializeField] private TextMeshProUGUI m_range;
        [SerializeField] private TextMeshProUGUI m_damage;
        [SerializeField] private TextMeshProUGUI m_atkSpd;

        private bool m_hasBeenDisplayed;

        private void Start()
        {
            Hide();
        }

        public void Show(WeaponData data, float baseAtkSpd)
        {
            m_canvasGroup.alpha = 1;
            if (!m_hasBeenDisplayed)
            {
                FillInfo(data, baseAtkSpd);
                m_hasBeenDisplayed = true;
            }
        }

        private void FillInfo(WeaponData data, float baseAtkSpd)
        {
            m_name.text = data.ItemID;
            m_rarity.text = $"Rarity:{data.Rarity.ToString()}";
            m_category.text = data.ItemCategory.ToString();
            m_range.text = $"Attack Range:{data.AttackRange}";
            m_damage.text = $"Attack Damage:{data.MinDamage} - {data.MaxDamage}";
            m_atkSpd.text = $"Attack Speed:{baseAtkSpd:F1}";
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
