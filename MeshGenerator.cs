using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;    

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //membuat array bertipe Vector3 dengan besar (xSize + 1) * (zSize + 1)
        //+1 karena untuk membuat satu garis dengan panjang X dibutuhkan X + 1 sebagai penutup
        //* * -> satu vertices membutuhkan dua edge
        //* * * -> dua vertices membutuhkan tiga edge

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .4f) * 1.4f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        //looping z hingga nilainya sama dengan z 
        //looping x hingga nilainya sama dengan x
        //variabel i untuk menyimpan indeks

        triangles = new int[xSize * zSize * 6];
        //dikali 6 karena tiap face membutuhkan 6 edge

        int vert = 0;
        int tris = 0;
        //variabel untuk tracing sudah pada vertices dan triangle ke berapa

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        //variabel triangle untuk membuat triangle pada mesh 
        //edge dibuat dari kiri -> atas -> bawah-kanan
        // 2    5  6
        // 1  3    4
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
