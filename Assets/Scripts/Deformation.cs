using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Deformation : MonoBehaviour
{
    [Range(1.5f, 5f)]
    public float radious = 1.5f;
    [Range(0.5f, 5f)]
    public float deformationStr = 0.5f;

    public Mesh mesh;
    public Vector3[] verticies, modifiedVerts;
    // Start is called before the first frame update
    public void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        verticies = mesh.vertices;
        modifiedVerts = mesh.vertices;
    }

    public void RecalculateMesh()
    {
        mesh.vertices = modifiedVerts;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    public void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int v = 0; v < modifiedVerts.Length; v++)
            {
                Vector3 distance = modifiedVerts[v] - hit.point;

                float smoothFactor = 2f;
                float force = deformationStr / (1f + hit.point.sqrMagnitude);

                if (distance.sqrMagnitude < radious)
                {
                    if (Input.GetMouseButton(0))
                    {
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.down * force) / smoothFactor;
                    }
                    else if(Input.GetMouseButton(1))
                    {
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.up * force) / smoothFactor;
                    }
                }
            }
        }
        RecalculateMesh();
    }
}

[CanEditMultipleObjects]
[CustomEditor(typeof(Deformation))]

public class DeformationEdit : Editor
{
    private void OnSceneGUI()
    {
        Deformation dfEdit = (Deformation)target;
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;
        dfEdit.Start();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int v = 0; v < dfEdit.modifiedVerts.Length; v++)
            {
                Vector3 distance = dfEdit.modifiedVerts[v] - hit.point;

                float smoothFactor = 2f;
                float force = dfEdit.deformationStr / (1f + hit.point.sqrMagnitude);

                if (distance.sqrMagnitude < dfEdit.radious)
                {
                    if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                    {
                        dfEdit.modifiedVerts[v] = dfEdit.modifiedVerts[v] + (Vector3.down * force) / smoothFactor;
                    }
                    else if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
                    {
                        dfEdit.modifiedVerts[v] = dfEdit.modifiedVerts[v] + (Vector3.up * force) / smoothFactor;
                    }
                }
            }
        }
        dfEdit.RecalculateMesh();
    }
}
