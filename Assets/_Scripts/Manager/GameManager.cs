using SGGames.Scripts.Common;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEvent m_gameEvent;

        private void Start()
        {
            m_gameEvent.AddListener(OnGameEvent);
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnGameEvent);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;    
        }

        private void UnpauseGame()
        {
            Time.timeScale = 1;
        }

        private void OnGameEvent(GameEventType eventType)
        {
            if (eventType == GameEventType.PAUSED)
            {
                PauseGame();
            }
            else if (eventType == GameEventType.UNPAUSED)
            {
                UnpauseGame();
            }
        }
    }
}
