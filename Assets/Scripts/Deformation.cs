using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformation : MonoBehaviour
{

    private Mesh mesh;
    private MeshCollider meshCol;
    private Vector3[] verticies, modifiedVerts;
    private float rad;
    public float height = 1f;
    [Range(0, 68)]
    public int voidIndexStart = 0;
    [Range(0, 68)]
    public int voidIndexEnd = 0;
    // Start is called before the first frame update
    public void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCol = GetComponent<MeshCollider>();
        verticies = mesh.vertices;
        rad = 180f / verticies.Length;
    }
    private void Update()
    {
        for (int i = 0 + voidIndexStart; i < verticies.Length - voidIndexEnd; i++)
        {
            verticies[i].y = -Mathf.Sin(rad * i * Mathf.Deg2Rad) / height;
        }
        mesh.vertices = verticies;
        mesh.RecalculateNormals();
        meshCol.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
