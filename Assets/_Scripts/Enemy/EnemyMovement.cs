
using System;
using SGGames.Scripts.Attribute;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public enum MOVEMENT_MODE
    {
        TOWARD_DIRECTION,
        FOLLOW_TARGET,
    }
    
    public class EnemyMovement : EnemyBehavior
    {
        [SerializeField] protected MOVEMENT_MODE m_movementMode = MOVEMENT_MODE.TOWARD_DIRECTION;
        [SerializeField] protected bool m_canMove;
        [SerializeField] protected float m_initialSpeed;
        [SerializeField] protected float m_currentSpeed;
        [SerializeField] protected Vector2 m_direction;
        [SerializeField] protected LayerMask m_obstacleLayerMask;
        [SerializeField][ReadOnly] protected Transform m_followingTarget;
        
        private readonly float m_raycastDistance = 0.2f;
        protected BoxCollider2D m_boxCollider;
        private ObstacleChecker m_obstacleChecker;

        public Action OnHitObstacle;
        
        protected override void Start()
        {
            base.Start();
            ResetSpeed();
            m_boxCollider = GetComponent<BoxCollider2D>();
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

        public virtual void StartMoving()
        {
            m_canMove = true;
        }

        public virtual void StopMoving(bool shouldResetDirection = false)
        {
            m_canMove = false;
            if (shouldResetDirection)
            {
                m_direction = Vector2.zero;
            }
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
            if (!m_canMove) return;
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
        
        #if UNITY_EDITOR
        public void ApplyMovementData(float moveSpeed)
        {
            m_initialSpeed = moveSpeed;
        }
        
        #endif
    }
}

