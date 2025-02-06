using MoreMountains.Feedbacks;
using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class LevelUpVFXController : MonoBehaviour
    {
        [SerializeField] private MMF_Player m_levelUpStartVFX;
        [SerializeField] private MMF_Player m_levelUpStopVFX;
        [SerializeField] private ActionEvent m_levelUpStartVFXEvent;
        [SerializeField] private ActionEvent m_levelUpStopVFXEvent;
        [SerializeField] private IntEvent m_playerLevelUpEvent;

        private void Start()
        {
            m_levelUpStopVFXEvent.AddListener(OnStopVFX);
            m_levelUpStartVFXEvent.AddListener(OnStartLevelUpVFX);
            m_levelUpStartVFX.Events.OnComplete.AddListener(OnCompleteVFX);
        }
        
        private void OnDestroy()
        {
            m_levelUpStopVFXEvent.RemoveListener(OnStopVFX);
            m_levelUpStartVFX.Events.OnComplete.RemoveListener(OnCompleteVFX);
            m_levelUpStartVFXEvent.RemoveListener(OnStartLevelUpVFX);
        }

        private void OnStartLevelUpVFX()
        {
            m_levelUpStartVFX.PlayFeedbacks();
        }
        
        private void OnStopVFX()
        {
            m_levelUpStopVFX.PlayFeedbacks();
        }

        private void OnCompleteVFX()
        {
            m_playerLevelUpEvent?.Raise(InGameProgressManager.Instance.CurrentLevel);
        }
    }
}
