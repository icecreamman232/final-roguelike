using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerAim : PlayerBehavior
    {
        [SerializeField] private Vector2 m_aimDirection;
        [SerializeField] private Transform m_aimArrowTransform;
        [SerializeField] private Transform m_aimArrowVisualTransform;
        [SerializeField] private float m_offsetFromCenter;
        
        //Its because the sprite designed to look up
        private readonly float m_offsetAngle = -90;
        
        public Vector2 AimDirection => m_aimDirection;
        
        
        private Camera m_camera;
        protected override void Start()
        {
            base.Start();
            m_camera = Camera.main;
        }

        protected override void Update()
        {
            if (!m_isAllow) return;
            HandleMouseInput();
            UpdateAimArrowVisual();
        }

        private void HandleMouseInput()
        {
            var worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            m_aimDirection = (worldPos - transform.position).normalized;
        }

        private void UpdateAimArrowVisual()
        {
            m_aimArrowTransform.position = transform.position + (Vector3)m_aimDirection * m_offsetFromCenter;
            m_aimArrowVisualTransform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg + m_offsetAngle, Vector3.forward);
        }

        public override void OnPlayerFreeze(bool isFrozen)
        {
            ToggleAllow(!isFrozen);
            base.OnPlayerFreeze(isFrozen);
        }
    }
}

