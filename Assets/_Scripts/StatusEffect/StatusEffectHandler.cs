using System.Collections.Generic;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Modifiers;
using UnityEngine;

namespace SGGames.Scripts.StatusEffects
{
    public class StatusEffectHandler : MonoBehaviour
    {
        [SerializeField] protected StatusEffectEvent m_statusEffectEvent;
        [SerializeField] protected ModifierHandler m_modifierHandler;
        private Health m_health;
        private Dictionary<StatusEffectType, StatusEffectInstance> m_statusEffectInstanceDictionary;
        private List<StatusEffectType> m_toBeRemoveList;
        
        public ModifierHandler ModifierHandler => m_modifierHandler;
        
        private void Start()
        {
            m_health = GetComponentInParent<Health>();
            m_statusEffectInstanceDictionary = new Dictionary<StatusEffectType, StatusEffectInstance>();
            m_toBeRemoveList = new List<StatusEffectType>();
        }

        public virtual void AddStatus(StatusEffectData data, GameObject source = null)
        {
            if (m_statusEffectInstanceDictionary.ContainsKey(data.StatusEffectType))
            {
                //Increase stack if possible
                m_statusEffectInstanceDictionary[data.StatusEffectType].IncreaseStack();
            }
            else
            {
                //Add new status
                var newInstance = new StatusEffectInstance
                        .StatusEffectBuilder()
                    .WithHealth(m_health)
                    .Build(this, data);
                newInstance.ApplyStatus();
                m_statusEffectInstanceDictionary.Add(data.StatusEffectType, newInstance);
            }
        }

        public void UpdateStatusUI(StatusEffectType effectType,int stackAmount)
        {
            m_statusEffectEvent.Raise(effectType,stackAmount);
        }

        protected virtual void Update()
        {
            foreach (StatusEffectInstance statusEffectInstance in m_statusEffectInstanceDictionary.Values)
            {
                if(statusEffectInstance == null) continue;
                if (statusEffectInstance.MarkToBeRemoved)
                {
                    m_toBeRemoveList.Add(statusEffectInstance.StatusEffectData.StatusEffectType);
                    continue;
                }
                statusEffectInstance.UpdateInstance(Time.time);
            }

            foreach (var status in m_toBeRemoveList)
            {
                m_statusEffectInstanceDictionary.Remove(status);
            }
            m_toBeRemoveList.Clear();
        }
    }
}