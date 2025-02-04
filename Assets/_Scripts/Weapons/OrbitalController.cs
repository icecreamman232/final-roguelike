using UnityEngine;
using UnityEngine.Events;


namespace SGGames.Scripts.Weapons
{
    public class OrbitalController : MonoBehaviour
    {
        [SerializeField] protected float m_orbitSpeed;
        [SerializeField] protected bool m_orbitClockwise;
        [SerializeField] protected Transform[] m_orbitObjects;
        [SerializeField] protected UnityEvent m_OnStartOrbital;
        [SerializeField] protected UnityEvent m_OnStopOrbital;

        protected float m_currentAngle;
        protected bool m_isRotating;
        
        
        [ContextMenu("Start Orbital")]
        public void StartRotating()
        {
            m_OnStartOrbital?.Invoke();
            m_isRotating = true;
            m_currentAngle = 0;
        }

        [ContextMenu("Stop Orbital")]
        public void StopRotating()
        {
            m_OnStopOrbital?.Invoke();
            m_isRotating = false;
            foreach (var orbitObject in m_orbitObjects)
            {
                orbitObject.localRotation = Quaternion.AngleAxis(0,Vector3.forward);
            }
        }

        protected virtual void Update()
        {
            if (!m_isRotating) return;

            if (m_orbitClockwise)
            {
                m_currentAngle -= m_orbitSpeed * Time.deltaTime;
            }
            else
            {
                m_currentAngle += m_orbitSpeed * Time.deltaTime;
            }
            
            if (m_currentAngle >= 360)
            {
                m_currentAngle = 0;
            }
            
            foreach (var orbitObject in m_orbitObjects)
            {
                orbitObject.localRotation = Quaternion.AngleAxis(m_currentAngle,Vector3.forward);
            }
        }
    }
}
