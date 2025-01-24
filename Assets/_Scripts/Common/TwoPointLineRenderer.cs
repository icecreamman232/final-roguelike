using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class TwoPointLineRenderer : MonoBehaviour
    {
        private LineRenderer m_lineRenderer;

        private void Awake()
        {
            m_lineRenderer = GetComponent<LineRenderer>();
        }

        public void UpdateLine(Vector2 start, Vector2 end)
        {
            m_lineRenderer.SetPosition(0,start);
            m_lineRenderer.SetPosition(1,end);
        }
    }
}

