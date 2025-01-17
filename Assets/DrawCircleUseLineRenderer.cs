
using UnityEngine;


namespace SGGames.Scripts.Common
{
    public class DrawCircleUseLineRenderer : MonoBehaviour
    {
        [SerializeField] private int m_steps;
        [SerializeField] private float m_radius;
        [SerializeField] private float m_thick;
        
        private LineRenderer m_lineRenderer;
        
        private void Start()
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            DrawCircle(m_steps, m_radius);
        }

        private void DrawCircle(int steps, float radius)
        {
            m_lineRenderer.positionCount = steps;
            m_lineRenderer.startWidth = m_thick;
            m_lineRenderer.endWidth = m_thick;
      
            for (int currentStep = 0; currentStep < steps; currentStep++)
            {
                float circumferenceProgress = (float)currentStep/steps;
                float currentRadian = circumferenceProgress * 2 * Mathf.PI ;
         
                var xScaled = Mathf.Cos(currentRadian);
                var yScaled= Mathf.Sin(currentRadian);
         
                var x = xScaled * radius;
                var y = yScaled * radius;
         
                var currentPos = new Vector3(x,y,0) + transform.position;
                m_lineRenderer.SetPosition(currentStep, currentPos);
                m_lineRenderer.loop = true;
            }
        }

        public void SetCircleRadius(float radius)
        {
            m_radius = radius;
        }
    }
}

