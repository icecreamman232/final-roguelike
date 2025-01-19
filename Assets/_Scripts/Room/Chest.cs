using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Pickables;
using SGGames.Scripts.Player;
using TMPro;
using UnityEngine;


namespace SGGames.Scripts.Rooms
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [Header("Chest")]
        [SerializeField] private InteractType m_type;
        [SerializeField] private ChestType m_chestType;
        [SerializeField] private bool m_hasInteract;
        [SerializeField] private TextMeshPro m_promptText;
        [SerializeField] private GameObject m_model;
        [SerializeField] private BoxCollider2D m_collider2D;
        [SerializeField] private InteractionLoot m_loot;
        [SerializeField] private GameEvent m_gameEvent;

        [Header("Drop Chance Item")] 
        [SerializeField] private EquipmentTierProgressionData m_equipmentTierProgressionData;
        

        private readonly int m_keyNumberToUnlockLegendary = 1;
        private PlayerInteract m_playerInteract;
        public ChestType ChestType => m_chestType;

        private void Start()
        {
            m_model.SetActive(false);
            m_collider2D.enabled = false;
            m_gameEvent.AddListener(OnReceiveGameEvent);
        }

        private void OnReceiveGameEvent(GameEventType eventType)
        {
            if (eventType != GameEventType.ROOM_CLEARED) return;
            //TODO: Play chest drop down animation here
            m_model.SetActive(true);
            m_collider2D.enabled = true;
            m_gameEvent.RemoveListener(OnReceiveGameEvent);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                m_hasInteract = true;
                if (m_playerInteract == null)
                {
                    m_playerInteract = other.GetComponent<PlayerInteract>();
                }
                m_playerInteract.AssignInteraction(m_type, this);
                ShowPrompt();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_hasInteract && other.CompareTag("Player"))
            {
                m_hasInteract = false;
                m_playerInteract.AssignInteraction(InteractType.None, null);
                HidePrompt();
            }
        }

        private void ShowPrompt()
        {
            m_promptText.gameObject.SetActive(true);
        }

        private void HidePrompt()
        {
            m_promptText.gameObject.SetActive(false);
        }

        public void Interact()
        {
            HidePrompt();
            
            m_loot.SpawnLoot(transform.parent,m_equipmentTierProgressionData.GetEquipmentRarity(LevelManager.Instance.CurrentAreaIndex));
            CurrencyManager.Instance.ConsumeKey(m_keyNumberToUnlockLegendary);
            this.gameObject.SetActive(false);
        }
    }
}

