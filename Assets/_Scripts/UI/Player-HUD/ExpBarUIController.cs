using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ExpBarUIController : MonoBehaviour
    {
        [SerializeField] private UpdateExpBarEvent m_UpdateExpBarEvent;
        [SerializeField] private Image m_progressBar;
        [SerializeField] private TextMeshProUGUI m_lvlText;

        private void Awake()
        {
            m_UpdateExpBarEvent.AddListener(OnUpdateExpBar);
        }

        private void OnDestroy()
        {
            m_UpdateExpBarEvent.RemoveListener(OnUpdateExpBar);
        }

        private void OnUpdateExpBar(int current, int max, int level)
        {
            m_progressBar.fillAmount = MathHelpers.Remap(current, 0, max, 0, 1);
            m_lvlText.text = $"{level.ToString()}";
        }
    }
}

