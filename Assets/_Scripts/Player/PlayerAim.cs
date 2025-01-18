using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerAim : PlayerBehavior
    {
        [SerializeField] private Vector2 m_aimDirection;
        [SerializeField] private AimTransform[] m_aimPositionTransforms;
        [SerializeField] private AimTransform[] m_aimRotationTransforms;
        
        private Camera m_camera;
        private AimController m_aimController;
        public Vector2 AimDirection => m_aimDirection;
        
        protected override void Start()
        {
            base.Start();
            m_camera = Camera.main;
            m_aimController = new AimController(this.transform);

            //Register related transforms which need to be updated when aiming changes
            for (int i = 0; i < m_aimPositionTransforms.Length; i++)
            {
                m_aimController.RegisterPositionTransform(m_aimPositionTransforms[i]);
            }
            for (int i = 0; i < m_aimRotationTransforms.Length; i++)
            {
                m_aimController.RegisterRotationTransform(m_aimRotationTransforms[i]);    
            }
        }

        protected override void Update()
        {
            if (!m_isAllow) return;
            HandleMouseInput();
            m_aimController.UpdatePosition(m_aimDirection);
            m_aimController.UpdateRotation(m_aimDirection);
        }

        private void HandleMouseInput()
        {
            var worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            m_aimDirection = (worldPos - transform.position).normalized;
        }

        public override void OnPlayerFreeze(bool isFrozen)
        {
            ToggleAllow(!isFrozen);
            base.OnPlayerFreeze(isFrozen);
        }
    }
}

