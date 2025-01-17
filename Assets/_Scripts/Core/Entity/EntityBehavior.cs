using UnityEngine;

namespace SGGames.Scripts.Core
{
    /// <summary>
    /// Base class for any entity in game included: player, enemy,boss and NPC
    /// </summary>
    public abstract class EntityBehavior : MonoBehaviour
    {
        [SerializeField] protected bool m_isAllow;

        public abstract void ToggleAllow(bool value);

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            
        }
    }
}
