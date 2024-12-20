using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Player;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class CharacterInfoUI : MonoBehaviour
    {
       [SerializeField] private CanvasGroup m_canvasGroup;
       [SerializeField] private TextMeshProUGUI m_characterName;
       [SerializeField] private TextMeshProUGUI m_strengthNumberText;
       [SerializeField] private TextMeshProUGUI m_agilityNumberText;
       [SerializeField] private TextMeshProUGUI m_intelligenceNumberText;
       [SerializeField] private OpenCharacterInfoUIEvent m_openCharacterInfoUIEvent;

       private PlayerAttributeController m_playerAttributeController;
       
       private void Start()
       {
           Hide();
           m_openCharacterInfoUIEvent.AddListener(OnOpenCharacterInfoUI);
       }
       
       private void OnDestroy()
       {
           m_openCharacterInfoUIEvent.RemoveListener(OnOpenCharacterInfoUI);
       }
       
       private void OnOpenCharacterInfoUI(bool isOpen, PlayerAttributeController playerAttributeController)
       {
           if (isOpen)
           {
               m_playerAttributeController = playerAttributeController;
               FillInfo();
               Show();
           }
           else
           {
               Hide();
           }
       }

       private void FillInfo()
       {
           m_characterName.text = $"{m_playerAttributeController.HeroName} - Level {InGameProgressManager.Instance.CurrentLevel}";
           m_strengthNumberText.text = m_playerAttributeController.StrengthPoints.ToString("F0");
           m_agilityNumberText.text = m_playerAttributeController.AgilityPoints.ToString("F0");
           m_intelligenceNumberText.text = m_playerAttributeController.IntelligencePoints.ToString("F0");
       }
       

       private void Show()
       {
           m_canvasGroup.alpha = 1;
           m_canvasGroup.interactable = true;
           m_canvasGroup.blocksRaycasts = true;
       }

       private void Hide()
       {
           m_canvasGroup.alpha = 0;
           m_canvasGroup.interactable = false;
           m_canvasGroup.blocksRaycasts = false;
       }
    }
}

