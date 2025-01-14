using System.Collections;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Enemies;
using SGGames.Scripts.Healths;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class SlideSlimeKingAbility : Ability
    {
        [Header("Slide Params")]
        [SerializeField] private float m_range;
        [SerializeField] private float m_slideSpeed;
        [SerializeField] [ReadOnly] private Vector2 m_slideDirection;
        [SerializeField] private BoxCollider2D m_boxCollider;
        [SerializeField] private float m_raycastDistance;
        [SerializeField] private LayerMask m_obstacleMask;
        [Header("Warning")]
        [SerializeField] private GameObject m_markSpriteObject;
        [SerializeField] private float m_warningDuration;
        [Header("Component Ref")]
        [SerializeField] private EnemyMovement m_enemyMovement;
        [SerializeField] private EnemyHealth m_enemyHealth;
        [SerializeField] private EnemyController m_enemyController;

        private Vector2 m_startSlidePos;
        
        public override void StartAbility()
        {
            m_enemyMovement.StopMoving(shouldResetDirection:true);
            m_enemyHealth.SetImmortal(true);
            FindDirection();
            base.StartAbility();
        }

        private void FindDirection()
        {
            m_slideDirection = (m_enemyController.CurrentBrain.Target.position - transform.position);
            m_startSlidePos = transform.position;
        }
        
        private bool CheckObstacle()
        {
            var hit = Physics2D.BoxCast(
                transform.position,
                m_boxCollider.size,0,m_slideDirection,m_raycastDistance,m_obstacleMask);
            return hit.collider != null;
        }

        protected override void PreTriggerState()
        {
            if (m_markSpriteObject.activeSelf) return;
            StartCoroutine(OnWarningPlayer());
        }

        private IEnumerator OnWarningPlayer()
        {
            m_markSpriteObject.SetActive(true);
            yield return new WaitForSeconds(m_warningDuration);
            m_markSpriteObject.SetActive(false);
            base.PreTriggerState();
            m_abilityState = AbilityState.TRIGGERING;
        }

        protected override void TriggeringState()
        {
            transform.Translate(m_slideDirection * (Time.deltaTime * m_slideSpeed));

            if (CheckObstacle())
            {
                StopAbility();
                return;
            }
            
            if (Vector2.Distance(transform.position, m_startSlidePos) >= m_range)
            {
                StopAbility();
                return;
            }
            base.TriggeringState();
        }

        protected override void PostTriggerState()
        {
            m_enemyHealth.SetImmortal(false);
            m_enemyMovement.StartMoving();
            m_slideDirection = Vector2.zero;
            base.PostTriggerState();
        }

#if UNITY_EDITOR
        [SerializeField] private bool m_isShowDebug;
        private void OnDrawGizmos()
        {
            if (!m_isShowDebug) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_range);
        }
        
        #endif
    }
}
