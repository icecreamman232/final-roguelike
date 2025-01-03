using System;
using UnityEngine;

namespace SGGames.Scripts.Common
{
    public class DrawCircleMesh : MonoBehaviour
    {
        [SerializeField] private int m_segments = 360;
        [SerializeField] private float m_radius = 1f;
        private MeshRenderer m_meshRenderer;
        private MeshFilter m_meshFilter;
        private float m_textureScale = 1f;
        private Vector3[] m_vertices;
        private Vector2[] m_uv;

        private void Start()
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
            m_meshFilter = GetComponent<MeshFilter>();
            DrawCircle();
        }

        private void Update()
        {
            UpdateCircle(m_radius);
        }

        public void DrawCircle()
        {
            Mesh mesh = new Mesh();
            m_meshFilter.mesh = mesh;

            m_vertices = new Vector3[m_segments + 1];
            m_uv = new Vector2[m_segments + 1];
            int[] triangles = new int[m_segments * 3];

            for (int i = 0; i < m_segments + 1; i++)
            {
                float angle = 2 * Mathf.PI * i / m_segments;
                float x = Mathf.Cos(angle) * m_radius;
                float y = Mathf.Sin(angle) * m_radius;
                
                // UV mapping
                m_uv[i] = new Vector2((x / (2 * m_radius) * m_textureScale + 0.5f), (y / (2 * m_radius) * m_textureScale + 0.5f));

                m_vertices[i] = new Vector3(x, y, 0);

                if (i > 0)
                {
                    triangles[(i - 1) * 3] = 0;
                    triangles[(i - 1) * 3 + 1] = i;
                    triangles[(i - 1) * 3 + 2] = i == m_segments ? 1 : i + 1;
                }
            }

            mesh.vertices = m_vertices;
            mesh.uv = m_uv;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            m_meshFilter.mesh = mesh;
        }

        private void UpdateCircle(float newRadius)
        {
            m_radius = newRadius;
            
            var currentMesh = m_meshFilter.mesh;
            // Calculate textureScale dynamically based on radius
            m_textureScale = m_radius;
            
            for (int i = 0; i < m_segments + 1; i++)
            {
                float angle = 2 * Mathf.PI * i / m_segments;
                float x = Mathf.Cos(angle) * m_radius;
                float y = Mathf.Sin(angle) * m_radius;
                
                m_uv[i] = new Vector2((x / (2 * m_radius) * m_textureScale + 0.5f), (y / (2 * m_radius) * m_textureScale + 0.5f));
        
                m_vertices[i] = new Vector3(x, y, 0);
            }
            currentMesh.vertices = m_vertices;
            currentMesh.uv = m_uv;
            currentMesh.RecalculateNormals();
            currentMesh.RecalculateBounds();

            m_meshFilter.mesh = currentMesh;
        }
    }
}
