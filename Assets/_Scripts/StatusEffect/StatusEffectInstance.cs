using System;
using SGGames.Scripts.Healths;
using SGGames.Scripts.Modifiers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.StatusEffects
{
    [Serializable]
    public class StatusEffectInstance
    {
        protected StatusEffectData m_statusEffectData;
        protected int m_currentStackNumber;
        protected float m_timeStart;
        protected StatusEffectHandler m_handler;
        protected Health m_health;
        protected float m_durationTimer;

        public bool MarkToBeRemoved;
        public StatusEffectData StatusEffectData => m_statusEffectData;

        public StatusEffectInstance(StatusEffectHandler handler,StatusEffectData data, Health health = null)
        {
            m_handler = handler;
            m_health = health;
            m_statusEffectData = data;
            m_currentStackNumber = 1;
            m_timeStart = Time.time;
            MarkToBeRemoved = false;
            m_durationTimer = data.Duration;
        }

        public void IncreaseStack()
        {
            if (m_durationTimer <= 0) return;
            if (m_currentStackNumber >= m_statusEffectData.MaxStack) return;
            m_currentStackNumber++;
            m_handler.UpdateStatusUI(m_statusEffectData.StatusEffectType,m_currentStackNumber);
        }
        
        public virtual void UpdateInstance(float currentTime)
        {
            if (MarkToBeRemoved) return;
            
            m_durationTimer -= Time.deltaTime;
            if (m_durationTimer <= 0)
            {
                Kill();
            }

            if ((currentTime - m_timeStart) >= m_statusEffectData.TickTime)
            {
                m_timeStart = currentTime;
                Tick();
                m_currentStackNumber--;
                m_handler.UpdateStatusUI(m_statusEffectData.StatusEffectType,m_currentStackNumber);
                if (m_currentStackNumber <= 0)
                {
                    Kill();
                }
            }
        }

        public virtual void ApplyStatus()
        {
            CauseInitialDamage();
            ApplyModifier();
            m_handler.UpdateStatusUI(m_statusEffectData.StatusEffectType,m_currentStackNumber);
        }

        protected virtual void ApplyModifier()
        {
            if (m_handler.IsForPlayer)
            {
                var playerHandler = m_handler as PlayerStatusEffectHandler;
                if (playerHandler == null)
                {
                    throw new NullReferenceException("PlayerStatusEffectHandler cannot be null");
                }
                for (int i = 0; i < m_statusEffectData.Modifiers.Length; i++)
                {
                    playerHandler.ModifierHandler.RegisterModifier(m_statusEffectData.Modifiers[i]);
                }
            }
            else
            {
                var enemyHandler = m_handler as EnemyStatusEffectHandler;
                if (enemyHandler == null)
                {
                    throw new NullReferenceException("EnemyStatusEffectHandler cannot be null");
                }
                for (int i = 0; i < m_statusEffectData.Modifiers.Length; i++)
                {
                    enemyHandler.ModifierHandler.RegisterModifier(m_statusEffectData.Modifiers[i]);
                }
            }
        }

        protected virtual void RemoveModifier()
        {
            if (m_handler.IsForPlayer)
            {
                var playerHandler = m_handler as PlayerStatusEffectHandler;
                if (playerHandler == null)
                {
                    throw new NullReferenceException("PlayerStatusEffectHandler cannot be null");
                }
                for (int i = 0; i < m_statusEffectData.Modifiers.Length; i++)
                {
                    playerHandler.ModifierHandler.UnregisterModifier(m_statusEffectData.Modifiers[i]);
                }
            }
            else
            {
                var enemyHandler = m_handler as EnemyStatusEffectHandler;
                if (enemyHandler == null)
                {
                    throw new NullReferenceException("EnemyStatusEffectHandler cannot be null");
                }
                for (int i = 0; i < m_statusEffectData.Modifiers.Length; i++)
                {
                    enemyHandler.ModifierHandler.UnregisterModifier(m_statusEffectData.Modifiers[i]);
                }
            }
        }

        protected virtual void CauseInitialDamage()
        {
            if (m_health == null) return;
            if (m_statusEffectData.InitialDamage <= 0) return;
            Debug.Log($"Status <color=orange>{m_statusEffectData.StatusEffectType}</color> cause <color=yellow>{m_statusEffectData.InitialDamage}</color> initial dmg.");
            m_health.TakeDamage(m_statusEffectData.InitialDamage,null,0);
        }
        
        protected virtual void Tick()
        {
            if (m_health == null) return;
            var tickDamage = GetDamage();
            Debug.Log($"Tick from <color=orange>{m_statusEffectData.StatusEffectType}</color> cause <color=yellow>{tickDamage}</color> dmg");
            m_health.TakeDamage(tickDamage,null,0);
        }
        
        protected virtual void Kill()
        {
            MarkToBeRemoved = true;
            RemoveModifier();
            m_currentStackNumber = 0; //Remove all stack on removing status effect
            m_handler.UpdateStatusUI(m_statusEffectData.StatusEffectType,m_currentStackNumber);
            Debug.Log($"<color=orange>Removed status effect {m_statusEffectData.StatusEffectType}</color>");
        }
        
        protected virtual float GetDamage()
        {
            var finalDamage= Random.Range(m_statusEffectData.MinDamage, m_statusEffectData.MaxDamage);
            finalDamage = StatusEffectData.DamageStackingType == DamageStackingType.Addition 
                ? finalDamage * m_currentStackNumber 
                : GetMultiplicationDamage(finalDamage,m_currentStackNumber);
            return finalDamage;
        }

        private float GetMultiplicationDamage(float damage, int stackNumber)
        {
            var currentDamage = damage;
            Debug.Log($"Raw damage {currentDamage}");
            for (int i = 0; i < stackNumber; i++)
            {
                currentDamage += m_currentStackNumber * m_statusEffectData.PercentPerStack * currentDamage;
            }
            Debug.Log($"After multiplication: {currentDamage}");
            return currentDamage;
        }

        public class StatusEffectBuilder
        {
            private Health m_health = null;
            
            public StatusEffectBuilder WithHealth(Health health)
            {
                m_health = health;
                return this;
            }

            public StatusEffectInstance Build(StatusEffectHandler handler, StatusEffectData data)
            {
                return new StatusEffectInstance(handler, data, m_health);
            }
        }
    }
}

