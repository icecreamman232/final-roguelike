using System;
using SGGames.Scripts.Common;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Manager;
using SGGames.Scripts.Pickables;
using SGGames.Scripts.Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] private float m_commonChance;
        [SerializeField] private float m_uncommonChance;
        [SerializeField] private float m_rareChance;
        [SerializeField] private float m_epicChance;
        [SerializeField] private float m_legendaryChance;
        

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

        private Rarity GetItemRarity()
        {
            var chance = Random.Range(0f, 100f);
            if (chance <= m_commonChance)
            {
                return Rarity.Common;
            }
            if (chance <= m_uncommonChance)
            {
                return Rarity.Uncommon;
            }
            
            if (chance <= m_rareChance)
            {
                return Rarity.Rare;
            }
            
            return chance <= m_epicChance ? Rarity.Epic : Rarity.Legendary;
        }

        [ContextMenu("Test")]
        private void Test()
        {
            IncreasePercent(Rarity.Legendary, 5);
        }

        private void RecomputeChance(Rarity rarityAdd)
        {
            if (rarityAdd == Rarity.Common)
            {
                var newUncommonChance  = m_uncommonChance /(m_uncommonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_commonChance);
                var newRareChance  = m_rareChance /(m_uncommonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_commonChance);
                var newEpicChance  = m_epicChance /(m_uncommonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_commonChance);
                var newLegendaryChance  = m_legendaryChance /(m_uncommonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_commonChance);

                m_uncommonChance = newUncommonChance;
                m_rareChance = newRareChance;
                m_epicChance = newEpicChance;
                m_legendaryChance = newLegendaryChance;
            }
            else if (rarityAdd == Rarity.Uncommon)
            {
                var newCommonChance  = m_commonChance /(m_commonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_uncommonChance);
                var newRareChance  = m_rareChance /(m_commonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_uncommonChance);
                var newEpicChance  = m_epicChance /(m_commonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_uncommonChance);
                var newLegendaryChance  = m_legendaryChance /(m_commonChance + m_rareChance + m_epicChance + m_legendaryChance) * (100 - m_uncommonChance);
                
                m_commonChance = newCommonChance;
                m_rareChance = newRareChance;
                m_epicChance = newEpicChance;
                m_legendaryChance = newLegendaryChance;
            }
            else if(rarityAdd == Rarity.Rare)
            {
                var newCommonChance  = m_commonChance /(m_commonChance + m_uncommonChance + m_epicChance + m_legendaryChance) * (100 - m_rareChance);
                var newUncommonChance  = m_uncommonChance /(m_commonChance + m_uncommonChance + m_epicChance + m_legendaryChance) * (100 - m_rareChance);
                var newEpicChance  = m_epicChance /(m_commonChance + m_uncommonChance + m_epicChance + m_legendaryChance) * (100 - m_rareChance);
                var newLegendaryChance  = m_legendaryChance /(m_commonChance + m_uncommonChance + m_epicChance + m_legendaryChance) * (100 - m_rareChance);
                
                m_commonChance = newCommonChance;
                m_uncommonChance = newUncommonChance;
                m_epicChance = newEpicChance;
                m_legendaryChance = newLegendaryChance;
            }
            else if (rarityAdd == Rarity.Epic)
            {
                var newCommonChance  = m_commonChance /(m_commonChance + m_uncommonChance + m_rareChance + m_legendaryChance) * (100 - m_epicChance);
                var newUncommonChance  = m_uncommonChance /(m_commonChance + m_uncommonChance + m_rareChance + m_legendaryChance) * (100 - m_epicChance);
                var newRare  = m_rareChance /(m_commonChance + m_uncommonChance + m_rareChance + m_legendaryChance) * (100 - m_epicChance);
                var newLegendaryChance  = m_legendaryChance /(m_commonChance + m_uncommonChance + m_rareChance + m_legendaryChance) * (100 - m_epicChance);
                
                m_commonChance = newCommonChance;
                m_uncommonChance = newUncommonChance;
                m_rareChance = newRare;
                m_legendaryChance = newLegendaryChance;
            }
            else if (rarityAdd == Rarity.Legendary)
            {
                var newCommonChance  = m_commonChance /(m_commonChance + m_uncommonChance + m_rareChance + m_epicChance) * (100 - m_legendaryChance);
                var newUncommonChance  = m_uncommonChance /(m_commonChance + m_uncommonChance + m_rareChance + m_epicChance) * (100 - m_legendaryChance);
                var newRare  = m_rareChance /(m_commonChance + m_uncommonChance + m_rareChance + m_epicChance) * (100 - m_legendaryChance);
                var newEpicChance  = m_epicChance /(m_commonChance + m_uncommonChance + m_rareChance + m_epicChance) * (100 - m_legendaryChance);
                
                m_commonChance = newCommonChance;
                m_uncommonChance = newUncommonChance;
                m_rareChance = newRare;
                m_epicChance = newEpicChance;
            }
        }

        public void Interact()
        {
            HidePrompt();
            
            m_loot.SpawnLoot(transform.parent,GetItemRarity());
            CurrencyManager.Instance.ConsumeKey(m_keyNumberToUnlockLegendary);
            this.gameObject.SetActive(false);
        }

        public void IncreasePercent(Rarity rarity, float increase)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    m_commonChance += increase;
                    break;
                case Rarity.Uncommon:
                    m_uncommonChance += increase;
                    break;
                case Rarity.Rare:
                    m_rareChance += increase;
                    break;
                case Rarity.Epic:
                    m_epicChance += increase;
                    break;
                case Rarity.Legendary:
                    m_legendaryChance += increase;
                    break;
            }
            RecomputeChance(rarity);
        }
    }
}

