
using System.Collections;
using SGGames.Scripts.Enemies;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class BigJumpSlimeKingAbility : Ability
    {
        [SerializeField] private EnemyMovement m_enemyMovement;
        [SerializeField] private EnemyController m_enemyController;
        
        [SerializeField] private float m_jumpUpHeight;
        [SerializeField] private float m_jumpUpSpeed;
        [SerializeField] private float m_jumpDownSpeed;

        private Vector2 m_initialPos;
        private Vector2 m_targetPos;
        
        public override void StartAbility()
        {
            m_enemyMovement.StopMoving(shouldResetDirection:true);
            base.StartAbility();
        }

        protected override void PreTriggerState()
        {
            if (m_abilityState != AbilityState.PRE_TRIGGER) return;
            StartCoroutine(OnJumpUp());
        }

        private IEnumerator OnJumpUp()
        {
            m_targetPos = m_enemyController.CurrentBrain.Target.position;
            m_initialPos = transform.position;
            var traveledUpDistance = 0f;
            while (traveledUpDistance < m_jumpUpHeight)
            {
                transform.Translate(Vector2.up * (m_jumpUpSpeed * Time.deltaTime));
                traveledUpDistance = Vector2.Distance(transform.position, m_initialPos);
                yield return null;
            }
            base.PreTriggerState();
        }

        protected override void TriggeringState()
        {
            if(m_abilityState != AbilityState.TRIGGERING) return;
            StartCoroutine(OnJumpDown());
        }

        private IEnumerator OnJumpDown()
        {
            while (transform.position != (Vector3)m_targetPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_targetPos, m_jumpDownSpeed * Time.deltaTime);
                yield return null;
            }
            StopAbility();
        }
    }
}

