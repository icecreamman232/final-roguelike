
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Healths;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Scripts.Player
{
    public enum PlayerDashState
    {
        Standby,
        Prepare,
        Dashing,
        Finished,
    }
    
    public class PlayerDash : PlayerBehavior
    {
        [SerializeField] private PlayerDashState m_dashState;
        [SerializeField] private InputContextEvent m_dashButtonPressedEvent;
        [SerializeField] private AnimationCurve m_dashSpeedCurve;

        private readonly float m_dashDistance = 4f;
        private readonly float m_dashSpeed = 10f;
        private readonly float m_acceleration = 4;
        private readonly int m_staminaCost = 1;

        private float m_startDashTime;
        private float m_dashTime;
        private Vector2 m_dashDirection;
        private Vector2 m_destination;
        private PlayerHealth m_playerHealth;
        private PlayerStamina m_playerStamina;
        private PlayerMovement m_playerMovement;
        
        public PlayerDashState DashState => m_dashState;

        protected override void Start()
        {
            m_playerStamina = ServiceLocator.GetService<PlayerStamina>();
            m_playerHealth = ServiceLocator.GetService<PlayerHealth>();
            m_playerMovement = ServiceLocator.GetService<PlayerMovement>();
            m_playerMovement.OnHitObstacle += OnHitObstacle;
            m_dashButtonPressedEvent.AddListener(OnDashButtonPressed);
            base.Start();
        }

        private void OnDestroy()
        {
            m_dashButtonPressedEvent.RemoveListener(OnDashButtonPressed);
            m_playerMovement.OnHitObstacle -= OnHitObstacle;
        }

        private void OnHitObstacle(bool isHit)
        {
            CancelDash();
        }
        private void OnDashButtonPressed(InputAction.CallbackContext context)
        {
            if (!m_playerStamina.HasStamina) return;
            m_dashState = PlayerDashState.Prepare;
        }

        protected override void Update()
        {
            if (!m_isAllow) return;
            
            switch (m_dashState)
            {
                case PlayerDashState.Standby:
                    StandByState();
                    break;
                case PlayerDashState.Prepare:
                    PrepareState();
                    break;
                case PlayerDashState.Dashing:
                    DashingState();
                    break;
                case PlayerDashState.Finished:
                    FinishedState();
                    break;
            }
            base.Update();
        }

        private void StandByState()
        {
            
        }
        
        private void PrepareState()
        {
            if (m_dashState != PlayerDashState.Prepare) return;
            m_playerHealth.SetImmortalFromDash(true);
            m_playerStamina.ConsumeStamina(m_staminaCost);
            m_destination = m_playerMovement.LastDirection * m_dashDistance + (Vector2)transform.position;
            m_dashDirection = m_playerMovement.LastDirection;

            m_dashState = PlayerDashState.Dashing;
        }
        
        private void DashingState()
        {
            //Cancel dash if player want to move to other directions
            if (m_dashDirection != m_playerMovement.LastDirection)
            {
                CancelDash();
                return;
            }

            m_dashTime = Time.time - m_startDashTime;
            var accelTime = m_dashSpeedCurve.Evaluate(m_dashTime);
            transform.position = Vector2.MoveTowards(transform.position,m_destination,(m_dashSpeed + accelTime * m_acceleration) * Time.deltaTime);
            if ((Vector2)transform.position == m_destination)
            {
                m_dashState = PlayerDashState.Finished;
            }
        }
        
        private void FinishedState()
        {
            m_playerHealth.SetImmortalFromDash(false);
            m_dashState = PlayerDashState.Standby;
        }

        private void CancelDash()
        {
            m_dashState = PlayerDashState.Finished;
        }
    }
}

