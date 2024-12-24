using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private InputContextEvent m_pauseMenuButtonPressed;
        [SerializeField] private BoolEvent m_showPauseMenu;
        [SerializeField] private CanvasGroup m_canvasGroup;

        private void Start()
        {
            m_showPauseMenu.AddListener(OnShowPauseMenu);
            Hide();
        }

        private void OnDestroy()
        {
            m_showPauseMenu.RemoveListener(OnShowPauseMenu);
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

        private void OnShowPauseMenu(bool isOpen)
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

        public void Resume()
        {
            m_pauseMenuButtonPressed.Raise(new InputAction.CallbackContext());
        }

        public void OpenOptionMenu()
        {
            
        }

        public void QuitGame()
        {
            
        }
    }
}

