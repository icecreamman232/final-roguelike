using System.Collections;
using SGGames.Scripts.Common;
using SGGames.Scripts.Core;
using SGGames.Scripts.Enemies;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace SGGames.Scripts.Abilities
{
    public class BigJumpSlimeKingAbility : Ability
    {
        [SerializeField] private EnemyMovement m_enemyMovement;
        [SerializeField] private EnemyController m_enemyController;
        [SerializeField] private AnimationParameter m_jumpAnticipateAnim;
        [SerializeField] private AnimationParameter m_jumpAnim;

        [SerializeField] private float m_range;
        [SerializeField] private float m_jumpUpHeight;
        [SerializeField] private float m_jumpUpSpeed;
        [SerializeField] private float m_jumpDuration;
        [SerializeField] private bool m_showDebugRange;

        private Vector2 m_initialPos;
        private Vector2 m_targetPos;
        private Coroutine m_jumpRoutine;
        
        public override void StartAbility()
        {
            m_enemyMovement.StopMoving(shouldResetDirection:true);
            base.StartAbility();
        }

        protected override void PreTriggerState()
        {
            if (m_abilityState != AbilityState.PRE_TRIGGER) return;
            var distanceToTarget  = Vector2.Distance(transform.position, m_enemyController.CurrentBrain.Target.position);
            if (distanceToTarget > m_range)
            {
                m_abilityState = AbilityState.READY;
                return;
            }

            if (m_jumpRoutine != null)
            {
                StopCoroutine(m_jumpRoutine);
            }
            m_jumpRoutine = StartCoroutine(OnJumpUp());
        }

        private Vector2 FindMiddlePoint(Vector2 start, Vector2 end, float height)
        {
            var point = MathHelpers.GetPointAtDistanceFromLine(
                Vector2.Lerp(start, end, 0.5f),
                start,end,height);

            point.y *= start.x <= end.x ? 1 : -1;
            
            return point;
        }

        [ContextMenu("Test")]
        private void Test()
        {
            StartCoroutine(OnJumpUp());
        }

        private IEnumerator OnJumpUp()
        {
            m_abilityState = AbilityState.TRIGGERING;
            m_jumpAnticipateAnim.SetTrigger();
            m_jumpAnim.SetBool(true);
            yield return new WaitForSeconds(m_jumpAnticipateAnim.Duration);
            m_jumpAnim.SetBool(false);
            m_targetPos = m_enemyController.CurrentBrain.Target.position;
            m_initialPos = transform.position;
            var middlePoint = FindMiddlePoint(m_initialPos, m_targetPos, m_jumpUpHeight);
            var jumpTimer = 0f;
            var t = 0f;
            
            while (t < 1)
            {
                jumpTimer += Time.deltaTime * m_jumpUpSpeed;
                t = MathHelpers.Remap(jumpTimer,0,m_jumpDuration,0,1);
                transform.position = Vector2.Lerp(
                    Vector2.Lerp(m_initialPos, middlePoint, t),
                    Vector2.Lerp(middlePoint, m_targetPos, t),
                    t);
                yield return null;
            }

            
            base.PreTriggerState();
        }

        protected override void TriggeringState()
        {
            m_abilityState = AbilityState.POST_TRIGGER;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_range);
        }
    }
}

