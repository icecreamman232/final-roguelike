using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Managers;
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
       [SerializeField] private TextMeshProUGUI m_attackDamageText;
       [SerializeField] private TextMeshProUGUI m_criticalChanceText;
       [SerializeField] private TextMeshProUGUI m_criticalDamageText;
       [SerializeField] private TextMeshProUGUI m_armorText;
       [SerializeField] private TextMeshProUGUI m_dodgeRateText;
       [SerializeField] private TextMeshProUGUI m_hpRegenText;
       [SerializeField] private TextMeshProUGUI m_manaRegenText;
       [SerializeField] private OpenCharacterInfoUIEvent m_openCharacterInfoUIEvent;

       private PlayerAttributeController m_playerAttributeController;
       private PlayerDamageComputer m_playerDamageComputer;
       private PlayerHealth m_playerHealth;
       private PlayerMana m_playerMana;
       
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
               if (m_playerDamageComputer == null)
               {
                   m_playerDamageComputer = LevelManager.Instance.PlayerRef.GetComponent<PlayerDamageComputer>();
               }

               if (m_playerHealth == null)
               {
                   m_playerHealth = ServiceLocator.GetService<PlayerHealth>();
               }

               if (m_playerMana == null)
               {
                   m_playerMana = ServiceLocator.GetService<PlayerMana>();
               }
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

           m_attackDamageText.text = $"{m_playerDamageComputer.TotalMinDamage:F0}-{m_playerDamageComputer.TotalMaxDamage:F0}";
           m_criticalChanceText.text = $"{m_playerDamageComputer.CriticalChance:F2}%";
           m_criticalDamageText.text = $"{(m_playerDamageComputer.CriticalDamage * 100f):F2}%";
           
           m_armorText.text = m_playerHealth.Armor.ToString("F0");
           m_dodgeRateText.text = m_playerHealth.DodgeRate.ToString("F0");
           m_hpRegenText.text = m_playerHealth.HPRegenerationRate.ToString();
           m_manaRegenText.text = m_playerMana.ManaRegenerateRate.ToString();
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

