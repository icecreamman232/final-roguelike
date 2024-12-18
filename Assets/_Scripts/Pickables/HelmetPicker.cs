
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Pickables
{
    public class HelmetPicker : ItemPicker
    {
        [SerializeField] private HelmetPickerHUD m_pickerHUD;
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

