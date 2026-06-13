using UnityEngine;

public class Outlin : MonoBehaviour
{
    /// <summary>
    /// Reverses normals of object so that it can look like an outline
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        // Use .mesh instead of .sharedMesh to avoid altering the original asset file
        Mesh mesh = meshFilter.mesh; 

        // Invert the Direction of Normals
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;
         // Reverse Triangle Winding Order (fixes backface culling visibility)
        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                // Swap the first and third vertex of each triangle
                int temp = triangles[i];
                triangles[i] = triangles[i + 2];
                triangles[i + 2] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }
    }

}
