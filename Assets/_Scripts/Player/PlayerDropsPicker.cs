
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Player
{
    public class PlayerDropsPicker : PlayerBehavior
    {
        [SerializeField] private LayerMask m_pickableLayerMask;
        [SerializeField] private float m_pickRadius;
        private Collider2D[] m_pickableColliders;

        protected override void Start()
        {
            base.Start();
            m_pickableColliders = new Collider2D[10];
        }

        protected override void Update()
        {
            if (!m_isAllow) return;
            
            Physics2D.OverlapCircleNonAlloc(transform.position, m_pickRadius,m_pickableColliders,m_pickableLayerMask);

            if (m_pickableColliders.Length > 0)
            {
                for (int i = 0; i < m_pickableColliders.Length; i++)
                {
                    if(m_pickableColliders[i] ==null) continue;
                    var pickable = m_pickableColliders[i].GetComponent<Pickable>();
                    pickable.Picking(this.transform);
                }
            }
        }
    }
}
