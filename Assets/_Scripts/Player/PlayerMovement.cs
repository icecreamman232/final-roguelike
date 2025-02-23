using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerMovement : PlayerBehavior,IGameService
    {
        [Header("Obstacle Test")]
        [SerializeField] private LayerMask m_obstacleLayerMask; 
        [SerializeField] private Vector2 m_playerSize;
        [SerializeField] private Vector2 m_direction;
        [Header("Speed")]
        [SerializeField] private float m_initialSpeed;
        [SerializeField] private float m_currentSpeed;
        [SerializeField] private Vector2Event m_playerInputDirectionEvent;

        private ObstacleChecker m_obstacleChecker;
        private readonly float m_raycastDistance = 0.2f;
        private bool m_canMove;
        private Vector2 m_lastDirection;

        public Vector2 LastDirection => m_lastDirection;
        public Vector2 Direction => m_direction;
        public float Speed => m_currentSpeed;
        public float InitialSpeed => m_initialSpeed;

        public Action<bool> OnHitObstacle;

        public void ToggleMovement(bool canMove)
        {
            m_canMove = canMove;
        }

        /// <summary>
        /// Add additional value to current speed.
        /// The value could be negative, which return to reduce speed
        /// </summary>
        /// <param name="addedSpeed"></param>
        public void ModifySpeed(float addedSpeed)
        {
            m_currentSpeed += addedSpeed;
        }

        public void ResetSpeed()
        {
            m_currentSpeed = m_initialSpeed;
        }


        private void Awake()
        {
            ServiceLocator.RegisterService<PlayerMovement>(this);
        }

        protected override void Start()
        {
            m_playerInputDirectionEvent.AddListener(OnReceivePlayerInputDirection);
            base.Start();
            ResetSpeed();
            ToggleMovement(true);
            m_obstacleChecker = new ObstacleChecker();
        }
        
        protected override void Update()
        {
            base.Update();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            if (!m_canMove) return;
            if (m_obstacleChecker.IsColliderObstacle(transform.position,m_playerSize,m_direction,m_raycastDistance,m_obstacleLayerMask))
            {
                OnHitObstacle?.Invoke(true);
                m_direction = Vector2.zero;
            }

            if (m_direction != Vector2.zero)
            {
                m_lastDirection = m_direction;
            }
            transform.Translate(m_direction * (m_currentSpeed * Time.deltaTime));
        }

        public override void OnPlayerFreeze(bool isFrozen)
        {
            if (isFrozen)
            {
                m_canMove = false;
                m_direction = Vector2.zero;
            }
            else
            {
                m_canMove = true;
            }
        }
        
        private void OnReceivePlayerInputDirection(Vector2 direction)
        {
            m_direction = direction;
        }

        private void OnDestroy()
        {
            m_playerInputDirectionEvent.RemoveListener(OnReceivePlayerInputDirection);
        }
    }
}

