using SGGames.Scripts.Data;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class WeaponPicker : ItemPicker
    {
        [SerializeField] private WeaponPickerHUD m_pickerHUD;
        
        protected override void ShowPrompt()
        {
            m_pickerHUD.Show((WeaponData)m_itemData);
            base.ShowPrompt();
        }

        protected override void HidePrompt()
        {
            m_pickerHUD.Hide();
            base.HidePrompt();
        }
    }
}

