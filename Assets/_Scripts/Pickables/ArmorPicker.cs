
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class ArmorPicker : ItemPicker
    {
        [SerializeField] private ArmorPickerHUD m_pickerHUD;
        public ArmorPickerHUD PickerHUD { set => m_pickerHUD = value; }

        protected override void ShowPrompt()
        {
            base.ShowPrompt();
            m_pickerHUD.Show(m_itemData);  
        }

        protected override void HidePrompt()
        {
            base.HidePrompt();
            m_pickerHUD.Hide();
        }
    }
}

