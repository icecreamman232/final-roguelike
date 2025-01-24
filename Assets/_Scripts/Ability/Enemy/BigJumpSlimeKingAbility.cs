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
        [SerializeField] private AnimationParameter m_landingAnim;
        [SerializeField] private GameObject m_warningSignPrefab;

        [SerializeField] private float m_range;
        [SerializeField] private float m_jumpUpHeight;
        [SerializeField] private float m_jumpUpSpeed;
        [SerializeField] private float m_jumpDuration;
        [SerializeField] private bool m_showDebugRange;

        private Vector2 m_initialPos;
        private Vector2 m_targetPos;
        private Coroutine m_jumpRoutine;
        private GameObject m_warningSign;

        protected override void Start()
        {
            m_warningSign = Instantiate(m_warningSignPrefab);
            m_warningSign.SetActive(false);
            base.Start();
        }

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

            m_abilityState = AbilityState.TRIGGERING;
        }

        private Vector2 FindMiddlePoint(Vector2 start, Vector2 end, float height)
        {
            var point = MathHelpers.GetPointAtDistanceFromLine(
                Vector2.Lerp(start, end, 0.5f),
                start,end,height);

            point.y *= start.x <= end.x ? 1 : -1;
            
            return point;
        }

        private IEnumerator OnJumpUp()
        {
            m_targetPos = m_enemyController.CurrentBrain.Target.position;
            m_initialPos = transform.position;
            
            m_jumpAnticipateAnim.SetTrigger();
            m_jumpAnim.SetBool(true);
            m_warningSign.transform.position = m_targetPos;
            m_warningSign.SetActive(true);
            
            yield return new WaitForSeconds(m_jumpAnticipateAnim.Duration);
            m_jumpAnim.SetBool(false);
            
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

            m_warningSign.SetActive(false);
            m_landingAnim.SetTrigger();
            yield return new WaitForSeconds(m_landingAnim.Duration);
            
            base.TriggeringState();
            m_abilityState = AbilityState.POST_TRIGGER;
        }

        protected override void TriggeringState()
        {
            if (m_jumpRoutine == null)
            {
                m_jumpRoutine = StartCoroutine(OnJumpUp());
            }
        }

        protected override void PostTriggerState()
        {
            if (m_jumpRoutine != null)
            {
                StopCoroutine(m_jumpRoutine);
                m_jumpRoutine = null;
            }
            base.PostTriggerState();
        }
        
#if UNITY_EDITOR
        [ContextMenu("Test")]
        private void Test()
        {
            StartCoroutine(OnJumpUp());
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_range);
        }
#endif
    }
}

