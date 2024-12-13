using SGGames.Scripts.Pickables;
using SGGames.Scripts.Player;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.Rooms
{
    public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractType m_type;   
        [SerializeField] private bool m_hasInteract;
        [SerializeField] private TextMeshPro m_promptText;
        [SerializeField] private InteractionLoot m_loot;
        
        private PlayerInteract m_playerInteract;
        
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
            m_loot.SpawnLoot(transform.parent);
            this.gameObject.SetActive(false);
        }
    }
}

