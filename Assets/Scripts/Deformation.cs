using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Deformation : MonoBehaviour
{

    public Mesh mesh;
    public MeshCollider meshCol;
    public Vector3[] verticies, modifiedVerts;
    public float rad;
    public float height = 1000.1f;
    [Range(0, 68)]
    public int voidIndexStart = 60;
    [Range(0, 68)]
    public int voidIndexEnd = 50;
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

[CanEditMultipleObjects]
[CustomEditor(typeof(Deformation))]

public class DeformationEdit : Editor
{
    private void OnSceneGUI()
    {
        Deformation df = (Deformation)target;

        df.Start();
        for (int i = 0 + df.voidIndexStart; i < df.verticies.Length - df.voidIndexEnd; i++)
        {
            df.verticies[i].y = -Mathf.Sin(df.rad * i * Mathf.Deg2Rad) / df.height;
        }
        df.mesh.vertices = df.verticies;
        df.mesh.RecalculateNormals();
        df.meshCol.GetComponent<MeshCollider>().sharedMesh = df.mesh;


    }
}
