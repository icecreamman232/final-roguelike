using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerMovement : PlayerBehavior
    {
        [SerializeField] private Vector2 m_direction;
        [SerializeField] private float m_initialSpeed;
        [SerializeField] private float m_currentSpeed;

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
        public void AddSpeed(float addedSpeed)
        {
            m_currentSpeed += addedSpeed;
        }

        /// <summary>
        /// Assign new value to current speed
        /// </summary>
        /// <param name="modifiedSpeed"></param>
        public void ModifiedSpeed(float modifiedSpeed)
        {
            m_currentSpeed = modifiedSpeed;
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

        private void UpdateMovement()
        {
            if (!m_canMove) return;
            transform.Translate(m_direction * (m_currentSpeed * Time.deltaTime));
        }
    }
}

