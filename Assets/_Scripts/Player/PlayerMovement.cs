using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerMovement : PlayerBehavior
    {
        [Header("Obstacle Test")]
        [SerializeField] private LayerMask m_obstacleLayerMask; 
        [SerializeField] private Vector2 m_playerSize;
        [SerializeField] private Vector2 m_direction;
        [Header("Speed")]
        [SerializeField] private float m_initialSpeed;
        [SerializeField] private float m_currentSpeed;

        private readonly float m_raycastDistance = 0.2f;
        private bool m_canMove;
        private PlayerInputAction m_playerInput;
        
        public Vector2 Direction => m_direction;
        public float Speed => m_currentSpeed;
        public float InitialSpeed => m_initialSpeed;

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
            Debug.Log($"<color=yellow>ModifySpeed: {addedSpeed}</color>");
        }

        /// <summary>
        /// Assign new value to current speed
        /// </summary>
        /// <param name="modifiedSpeed"></param>
        public void OverrideSpeed(float modifiedSpeed)
        {
            m_currentSpeed = modifiedSpeed;
            Debug.Log($"<color=yellow>OverrideSpeed: {modifiedSpeed}</color>");
        }

        public void ResetSpeed()
        {
            m_currentSpeed = m_initialSpeed;
        }        

        protected override void Start()
        {
            base.Start();
            m_playerInput = new PlayerInputAction();
            m_playerInput.Enable();

            ResetSpeed();
            ToggleMovement(true);
        }

        protected override void Update()
        {
            base.Update();
            HandleInput();
            UpdateMovement();
        }

        private void HandleInput()
        {
            m_direction = m_playerInput.Player.WASD.ReadValue<Vector2>();
        }

        private bool CheckObstacle()
        {
            var hit = Physics2D.BoxCast(
                transform.position,
                m_playerSize,0,m_direction,m_raycastDistance,m_obstacleLayerMask);
            return hit.collider != null;
        }

        private void UpdateMovement()
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

