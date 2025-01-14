using System.Collections;
using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class PickAbilityCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private BoolEvent m_openingAbilitySelectUIEvent;
        [SerializeField] private BoolEvent m_freezePlayerEvent;
        [SerializeField] private GameEvent m_gameEvent;

        private readonly float C_CLOSE_FEEDBACK_TIME = 0.5f;
        
        private void Start()
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_canvasGroup.alpha = 0;
            m_openingAbilitySelectUIEvent.AddListener(OnToggleMenu);
        }

        private void OnDestroy()
        {
            m_openingAbilitySelectUIEvent.RemoveListener(OnToggleMenu);
        }

        private void Show()
        {
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_canvasGroup.alpha = 1;
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
