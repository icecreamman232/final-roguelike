
using UnityEngine;

namespace SGGames.Scripts.Enemies
{
    public class EnemyMovement : EnemyBehavior
    {
        [SerializeField] protected bool m_canMove;
        [SerializeField] protected float m_initialSpeed;
        [SerializeField] protected float m_currentSpeed;
        [SerializeField] protected Vector2 m_direction;
        [SerializeField] protected LayerMask m_obstacleLayerMask;

        private readonly float m_raycastDistance = 0.2f;
        protected BoxCollider2D m_boxCollider;
        
        protected override void Start()
        {
            base.Start();
            ResetSpeed();
            m_boxCollider = GetComponent<BoxCollider2D>();
        }

        public void SetDirection(Vector2 dir)
        {
            m_direction = dir;
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

        public virtual void AddSpeed(float addAmount)
        {
            m_currentSpeed += addAmount;
        }

        public virtual void OverrideSpeed(float newSpeed)
        {
            m_currentSpeed = newSpeed;
        }

        protected override void Update()
        {
            base.Update();
            UpdateMovement();
        }
        
        private bool CheckObstacle()
        {
            var hit = Physics2D.BoxCast(
                transform.position,
                m_boxCollider.size,0,m_direction,m_raycastDistance,m_obstacleLayerMask);
            return hit.collider != null;
        }

        protected virtual void UpdateMovement()
        {
            if (!m_canMove) return;
            if (CheckObstacle())
            {
                m_direction = Vector2.zero;
            }
            transform.Translate(m_direction * (m_currentSpeed * Time.deltaTime));
        }
    }
}

