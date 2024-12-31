using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class StatusEffectUINode : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_stackNumber;
        private bool m_isShowing;
        
        private void Show()
        {
            this.gameObject.SetActive(true);    
        }
        
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void UpdateStack(int currentStackNumber)
        {
            if (!m_isShowing)
            {
                Show();
            }
            
            m_stackNumber.text = currentStackNumber.ToString();
            if (currentStackNumber <= 0)
            {
                Hide();
            }
        }
    }
}
