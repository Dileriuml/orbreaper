using System.Collections.Generic;
using UnityEngine;

namespace OrbReaper
{
    public class Destructable : MonoBehaviour
    {
        private Mesh objectMesh;

        private void Start()
        {
            objectMesh = GetComponent<MeshFilter>().mesh;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > 2)
            {
                // Get the point of impact
                var impactPoint = collision.contacts[0].point;

                // Get the object's triangles
                var triangles = objectMesh.triangles;

                // Divide the triangles into multiple pieces
                var pieces = DivideMesh(triangles);

                // Create new meshes for each piece
                for (var i = 0; i < pieces.Length; i++)
                {
                    var pieceMesh = new Mesh();
                    pieceMesh.vertices = objectMesh.vertices;
                    pieceMesh.triangles = pieces[i];
                    pieceMesh.RecalculateNormals();

                    // Create a new GameObject for the piece
                    GameObject newObject = new GameObject("Broken Piece");
                    newObject.AddComponent<MeshFilter>().mesh = pieceMesh;
                    newObject.AddComponent<MeshRenderer>();

                    // Apply force to the new object to make it fly away from the point of impact
                    newObject.AddComponent<Rigidbody>().AddExplosionForce(200f, impactPoint, 10f);
                    // Create an explosion effect
                    //Instantiate(explosionPrefab, impactPoint, Quaternion.identity);
                }

                // Destroy the original object
                Destroy(gameObject);
            }
        }

        private int[][] DivideMesh(int[] triangles)
        {
            var subMeshes = new List<int[]>();
            var subMesh = new int[triangles.Length / 2];
            var subMeshIndex = 0;
            for (var i = 0; i < triangles.Length; i += 3)
            {
                // You can use any logic to divide the mesh into smaller pieces
                // For example, you can use a random number to decide whether to add
                // the current triangle to the current submesh or to create a new one
                if (Random.value > 0.5f)
                {
                    subMesh[subMeshIndex++] = triangles[i];
                    subMesh[subMeshIndex++] = triangles[i + 1];
                    subMesh[subMeshIndex++] = triangles[i + 2];
                }
                else
                {
                    subMeshes.Add(subMesh);
                    subMesh = new int[triangles.Length / 2];
                    subMeshIndex = 0;
                }
            }
            subMeshes.Add(subMesh);

            return subMeshes.ToArray();

        }
    }
}