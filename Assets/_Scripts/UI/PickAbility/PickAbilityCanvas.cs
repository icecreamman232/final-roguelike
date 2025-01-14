using System.Collections;
using SGGames.Scripts.Abilities;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class PickAbilityCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private BoolEvent m_openingAbilitySelectUIEvent;
        [SerializeField] private BoolEvent m_freezePlayerEvent;
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private PlayerAbilityContainer m_abilityContainer;
        [SerializeField] private AbilityCardUI[] m_abilityCard;

        private readonly float C_CLOSE_FEEDBACK_TIME = 0.5f;
        private readonly int C_MAX_CARD_AMOUNT = 3;
        private bool m_aboutToClose;
        
        private void Start()
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_canvasGroup.alpha = 0;
            foreach (var abilityCard in m_abilityCard)
            {
                abilityCard.OnClickCard += OnClickCard;
            }
            m_openingAbilitySelectUIEvent.AddListener(OnToggleMenu);
        }

        private void OnDestroy()
        {
            foreach (var abilityCard in m_abilityCard)
            {
                abilityCard.OnClickCard -= OnClickCard;
            }
            m_openingAbilitySelectUIEvent.RemoveListener(OnToggleMenu);
        }

        private void Show()
        {
            m_aboutToClose = false;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_canvasGroup.alpha = 1;

            var abilityManager = ServiceLocator.GetService<AbilityRewardManager>();
            for (int i = 0; i < C_MAX_CARD_AMOUNT; i++)
            {
                FillIntoToCard(i, abilityManager.GetRandomAbility());
            }
        }

        private void FillIntoToCard(int index, SelectableAbility abilityID)
        {
            var data = m_abilityContainer.GetAbilityData(abilityID);
            m_abilityCard[index].FillInfo(abilityID, data.AbilityIcon,data.AbilityName,data.AbilityDesc);
        }

        private void OnClickCard(SelectableAbility abilityID)
        {
            if (m_aboutToClose) return;
            ServiceLocator.GetService<PlayerAbilityController>().AddAbility(abilityID);
            m_aboutToClose = true;
            Hide();
        }
        

        private void Hide()
        {
            StartCoroutine(OnHide());
        }
        
        private IEnumerator OnHide()
        {
            yield return new WaitForSecondsRealtime(C_CLOSE_FEEDBACK_TIME);
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_canvasGroup.alpha = 0;

            m_freezePlayerEvent.Raise(false);
            m_gameEvent.Raise(GameEventType.UNPAUSED);
        }

        private void OnToggleMenu(bool isOpen)
        {
            if (isOpen)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}
