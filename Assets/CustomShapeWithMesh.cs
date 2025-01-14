using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShapeWithMesh : MonoBehaviour
{
    public int VertexCount;
    
    private void Start()
    {
        Mesh mesh = new Mesh();
        
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0)
        };
        
        int[] triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }
}
