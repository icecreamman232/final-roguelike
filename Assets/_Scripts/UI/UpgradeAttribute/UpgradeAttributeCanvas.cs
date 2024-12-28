using System.Collections;
using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class UpgradeAttributeCanvas : MonoBehaviour
    {
        [SerializeField] private IntEvent m_levelUpEvent;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private BoolEvent m_freezePlayerEvent;
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private AddAttributeEvent m_addAttributeEvent;
        [SerializeField] private UpgradeAttributeCardUI[] m_upgradeCardList;
        
        private readonly float C_CLOSE_FEEDBACK_TIME = 0.3f;
        
        private void Start()
        {
            Hide();
            m_levelUpEvent.AddListener(OnLevelUp);
            foreach (var card in m_upgradeCardList)
            {
                card.OnSelectCard += OnSelectCard;
            }
        }

        private void OnDestroy()
        {
            m_levelUpEvent.RemoveListener(OnLevelUp);
            foreach (var card in m_upgradeCardList)
            {
                card.OnSelectCard -= OnSelectCard;
            }
        }

        private void Show()
        {
            StartCoroutine(OnDelayBeforeShow());
        }
        
        private IEnumerator OnDelayBeforeShow()
        {
            m_gameEvent.Raise(GameEventType.PAUSED_WITH_DELAY);
            yield return new WaitUntil(()=>GameManager.Instance.IsGamePaused);
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_freezePlayerEvent.Raise(true);
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

        private void FillDataToCard(int index, (UpgradeAttributeRate rate, AttributeType type) attributeReward)
        {
            m_upgradeCardList[index].Show(attributeReward);
        }
        
        private void OnSelectCard(int index)
        {
            m_addAttributeEvent.Raise(m_upgradeCardList[index].Reward);
            if (index == 0)
            {
                m_upgradeCardList[1].PlayNotSelectFeedback();
                m_upgradeCardList[2].PlayNotSelectFeedback();
            }
            else if (index == 1)
            {
                m_upgradeCardList[0].PlayNotSelectFeedback();
                m_upgradeCardList[2].PlayNotSelectFeedback();
            }
            else if (index == 2)
            {
                m_upgradeCardList[0].PlayNotSelectFeedback();
                m_upgradeCardList[1].PlayNotSelectFeedback();
            }
            Hide();
        }
        
        private void OnLevelUp(int level)
        {
            Show();
            FillDataToCard(0,AttributeRewardController.Instance.GetAttributeReward());
            FillDataToCard(1,AttributeRewardController.Instance.GetAttributeReward());
            FillDataToCard(2,AttributeRewardController.Instance.GetAttributeReward());
        }
    }
}

