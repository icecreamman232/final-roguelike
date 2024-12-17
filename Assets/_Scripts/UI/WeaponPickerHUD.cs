using SGGames.Scripts.Data;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WeaponPickerHUD : MonoBehaviour
    {
        [SerializeField] private ConstantData m_constantData;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private TextMeshProUGUI m_rarityType;
        [SerializeField] private TextMeshProUGUI m_categoryType;
        [SerializeField] private TextMeshProUGUI m_rangeValue;
        [SerializeField] private TextMeshProUGUI m_damageValue;
        [SerializeField] private TextMeshProUGUI m_atkSpdValue;

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
            m_name.color = m_constantData.GetRarityColor(data.Rarity);
            m_name.text = data.TranslatedName;
            
            m_rarityType.color = m_constantData.GetRarityColor(data.Rarity);
            m_rarityType.text = data.Rarity.ToString();
            
            m_categoryType.text = data.WeaponCategory.ToString();
            m_rangeValue.text = data.AttackRange.ToString("F1");
            m_damageValue.text = $"{data.MinDamage} - {data.MaxDamage}";
            m_atkSpdValue.text = baseAtkSpd.ToString("F1");
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
