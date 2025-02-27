using System;
using MoreMountains.Tools;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.VFX
{
    public class PlayerGhostVFX : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private MMPoolableObject m_ghostVFXInstance;
        [SerializeField] private Material m_ghostVFXMaterial;
        
        private float m_currentLifeTime;
        private Material m_ghostVFXMaterialInstance;
        private bool m_canUpdate;

        private void Start()
        {
            m_ghostVFXMaterialInstance = new Material(m_ghostVFXMaterial);
            m_spriteRenderer.material = m_ghostVFXMaterialInstance;
            m_ghostVFXMaterialInstance.SetFloat("_Transparency", 1);
        }

        private void OnEnable()
        {
            m_canUpdate = true;
            m_currentLifeTime = m_ghostVFXInstance.LifeTime;
            if (m_ghostVFXMaterialInstance != null)
            {
                m_ghostVFXMaterialInstance.SetFloat("_Transparency", 1);
            }
        }

        private void Update()
        {
            if (!m_canUpdate) return;
            m_currentLifeTime -= Time.deltaTime;
            m_ghostVFXMaterialInstance.SetFloat("_Transparency",MathHelpers.Remap(m_currentLifeTime,0,m_ghostVFXInstance.LifeTime,0,1));
        }
        
        private void OnDisable()
        {
            m_canUpdate = false;
        }
    }
}
