using System.Collections.Generic;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Player;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private OpenInventoryUIEvent m_openInventoryUI;
        [SerializeField] private OpenCharacterInfoUIEvent m_openCharacterInfoUI;

        private bool m_isCharacterInfoOpened;
        private bool m_isInventoryOpened;
        public bool IsGamePaused => Time.timeScale == 0;
        
        public bool IsCharacterInfoOpened => m_isCharacterInfoOpened;
        public bool IsInventoryOpened => m_isInventoryOpened;
        
        private void Start()
        {
            m_gameEvent.AddListener(OnGameEvent);
            m_openInventoryUI.AddListener(OnOpeningInventoryUI);
            m_openCharacterInfoUI.AddListener(OnOpeningCharacterInfoUI);
        }
        
        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnGameEvent);
            m_openInventoryUI.RemoveListener(OnOpeningInventoryUI);
            m_openCharacterInfoUI.RemoveListener(OnOpeningCharacterInfoUI);
        }
        
        private void PauseGame()
        {
            Time.timeScale = 0;    
        }

        private void UnpauseGame()
        {
            if (!m_isInventoryOpened && !m_isCharacterInfoOpened)
            {
                Time.timeScale = 1;
            }
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
        
        private void OnOpeningInventoryUI(bool isOpen, WeaponData arg2, HelmetData arg3, ArmorData arg4, GlovesData arg5, BootsData arg6, AccessoriesData arg7, CharmData arg8, List<ItemData> arg9)
        {
            m_isInventoryOpened = isOpen;
        }
        
        private void OnOpeningCharacterInfoUI(bool isOpen, PlayerAttributeController arg2)
        {
            m_isCharacterInfoOpened = isOpen;
        }
    }
}
