
using System;
using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.EditorExtensions;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public enum MOVEMENT_MODE
    {
        TOWARD_DIRECTION,
        FOLLOW_TARGET,
    }

    public enum EnemyMovementState
    {
        STOP,
        MOVING,
        KNOCK_BACK,
    }
    
    public class EnemyMovement : EnemyBehavior
    {
        [SerializeField] protected EnemyMovementState m_movementState;
        [SerializeField] protected MOVEMENT_MODE m_movementMode = MOVEMENT_MODE.TOWARD_DIRECTION;
        [SerializeField] protected float m_initialSpeed;
        [SerializeField] protected float m_currentSpeed;
        [SerializeField] protected Vector2 m_direction;
        [SerializeField] protected LayerMask m_obstacleLayerMask;
        [SerializeField][ReadOnly] protected Transform m_followingTarget;
        [SerializeField] protected float m_knockBackThreshold; //Threshold that allow knock back to be happened;
        
        private readonly float m_raycastDistance = 0.2f;
        protected BoxCollider2D m_boxCollider;
        private EnemyController m_enemyController;
        private ObstacleChecker m_obstacleChecker;
        private Coroutine m_knockBackCoroutine;
        private Coroutine m_stunCoroutine;
        private bool m_isStunned;
        private float m_currentStunDuration;

        public Action OnHitObstacle;
        
        protected override void Start()
        {
            base.Start();
            ResetSpeed();
            m_movementState = EnemyMovementState.STOP;
            m_boxCollider = GetComponent<BoxCollider2D>();
            m_enemyController = GetComponent<EnemyController>();
            m_obstacleChecker = new ObstacleChecker();
        }

        public void SetToFollowingTarget(Transform target)
        {
            m_movementMode = MOVEMENT_MODE.FOLLOW_TARGET;
            m_followingTarget = target;
        }
        
        public void SetDirection(Vector2 dir)
        {
            m_movementMode = MOVEMENT_MODE.TOWARD_DIRECTION;
            m_direction = dir;
            if (m_obstacleChecker.IsColliderObstacle(transform.position,m_boxCollider.size,m_direction,m_raycastDistance,m_obstacleLayerMask))
            {
                var hitTop = Physics2D.Raycast(transform.position,Vector2.up,m_raycastDistance,m_obstacleLayerMask);
                var hitBot = Physics2D.Raycast(transform.position,Vector2.down,m_raycastDistance,m_obstacleLayerMask);
                var hitLeft = Physics2D.Raycast(transform.position,Vector2.left,m_raycastDistance,m_obstacleLayerMask);
                var hitRight = Physics2D.Raycast(transform.position,Vector2.right,m_raycastDistance,m_obstacleLayerMask);


                if (hitTop || hitBot)
                {
                    m_direction.y = 0;
                }
                else if (hitLeft || hitRight)
                {
                    m_direction.x = 0;
                }
            }
        }

        public void ApplyKnockBack(Vector2 knockBackDirection,float knockBackForce, float duration)
        {
            if (knockBackForce <= m_knockBackThreshold) return;
            
            //Not override knock back with new one
            if (m_movementState == EnemyMovementState.KNOCK_BACK) return;
            
            m_knockBackCoroutine = StartCoroutine(KnockBackRoutine(knockBackDirection,knockBackForce, duration));
        }

        public void ApplyStun(float duration)
        {
            if (m_isStunned)
            {
                //Override current stun with "better" stun
                if (duration > m_currentStunDuration)
                {
                    m_currentStunDuration = duration;
                    StopCoroutine(m_stunCoroutine);
                    m_stunCoroutine = StartCoroutine(StunningRoutine(m_currentStunDuration));
                }
                return;
            }

            m_currentStunDuration = duration;
            m_stunCoroutine = StartCoroutine(StunningRoutine(duration));
        }

        public virtual void StartMoving()
        {
            m_movementState = EnemyMovementState.MOVING;
        }

        public virtual void StopMoving(bool shouldResetDirection = false)
        {
            if (shouldResetDirection)
            {
                m_direction = Vector2.zero;
            }

            m_movementState = EnemyMovementState.STOP;
        }

        public virtual void ResetSpeed()
        {
            m_currentSpeed = m_initialSpeed;
        }

        public virtual void ModifySpeed(float addAmount)
        {
            m_currentSpeed += addAmount;
        }

        protected override void Update()
        {
            if (!m_isAllow) return;
            UpdateMovement();
        }
        
        protected virtual void UpdateMovement()
        {
            if (m_isStunned)
            {
                StunningState();
                //While being stunned, enemy is still able to receive knock back
                if (m_movementState == EnemyMovementState.KNOCK_BACK)
                {
                    KnockBackState();
                }
                return;
            }
            
            switch (m_movementState)
            {
                case EnemyMovementState.STOP:
                    StopState();
                    break;
                case EnemyMovementState.MOVING:
                    MovingState();
                    break;
                case EnemyMovementState.KNOCK_BACK:
                    KnockBackState();
                    break;
            }
        }

        protected virtual void StopState()
        {
            
        }

        protected virtual void MovingState()
        {
            if (m_obstacleChecker.IsColliderObstacle(transform.position,m_boxCollider.size,m_direction,m_raycastDistance,m_obstacleLayerMask))
            {
                m_direction = Vector2.zero;
                OnHitObstacle?.Invoke();
            }
            
            if (m_movementMode == MOVEMENT_MODE.FOLLOW_TARGET)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_followingTarget.position, m_currentSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(m_direction * (m_currentSpeed * Time.deltaTime));
            }
        }

        protected virtual void KnockBackState()
        {
            
        }

        protected virtual IEnumerator KnockBackRoutine(Vector2 knockBackDirection,float knockBackSpeed, float duration)
        {
            var prevState = m_movementState;
            m_movementState = EnemyMovementState.KNOCK_BACK;
            var knockBackSpd = knockBackSpeed;
            var endTime = Time.time + duration;
            while (Time.time < endTime)
            {
                if (m_obstacleChecker.IsColliderObstacle(transform.position,m_boxCollider.size,m_direction,m_raycastDistance,m_obstacleLayerMask))
                {
                    m_direction = Vector2.zero;
                    OnHitObstacle?.Invoke();
                    break;
                }
                knockBackSpd -= Time.deltaTime;
                knockBackSpd = Mathf.Clamp(knockBackSpd,0,m_currentSpeed);
                transform.Translate(knockBackDirection * (knockBackSpd * Time.deltaTime));
                if (knockBackSpd <= 0)
                {
                    break;
                }
                yield return null;
            }

            m_movementState = prevState;
        }
        
        protected virtual void StunningState()
        {
            
        }

        protected virtual IEnumerator StunningRoutine(float duration)
        {
            m_isStunned = true;
            m_enemyController.CurrentBrain.BrainActive = false;
            yield return new WaitForSeconds(duration);
            m_isStunned = false;
            m_enemyController.CurrentBrain.BrainActive = true;
        }
        
        #if UNITY_EDITOR
        public void ApplyMovementData(float moveSpeed)
        {
            m_initialSpeed = moveSpeed;
        }
        
        #endif
    }
}

