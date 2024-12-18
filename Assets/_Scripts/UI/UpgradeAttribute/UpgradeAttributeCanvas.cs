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
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_freezePlayerEvent.Raise(true);
            m_gameEvent.Raise(GameEventType.PAUSED);
        }

        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
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

