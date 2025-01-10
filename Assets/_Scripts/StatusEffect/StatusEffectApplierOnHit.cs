using SGGames.Scripts.Damages;
using UnityEngine;


namespace SGGames.Scripts.StatusEffects
{
    /// <summary>
    /// Apply status on damage hit
    /// </summary>
    [RequireComponent(typeof(DamageHandler))]
    public class StatusEffectApplierOnHit : MonoBehaviour
    {
        [SerializeField] private StatusEffectData m_StatusEffectData;
        private DamageHandler m_damageHandler;

        private void Start()
        {
            m_damageHandler = GetComponent<DamageHandler>();
            m_damageHandler.OnHitDamageable += OnHitDamageable;
        }

        private void OnHitDamageable(GameObject target)
        {
            var statusEffectHandler = target.GetComponentInChildren<StatusEffectHandler>();
            if (statusEffectHandler == null)
            {
                statusEffectHandler = target.GetComponent<StatusEffectHandler>();
            }

            if (statusEffectHandler != null)
            {
                statusEffectHandler.AddStatus(m_StatusEffectData,this.gameObject);
            }
        }
    }
}

