using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarbonFox.Volumetrics
{
    public class Volume3D : MonoBehaviour
    {
        public Vector3 size;
        public Vector3Int density;
        [HideInInspector] public Vector3Int count;

        public Vertex[,,] vertices;
        public Edge[,,][] edges;
        public Polyhedron[] polyhedrons;

        public float volume;

        private void Awake()
        {
            Volume3DFactory.Populate(this);
            RecalculateVolume();
        }

        public void RecalculateVolume()
        {
            volume = 0f;
            foreach (var poly in polyhedrons)
                volume += poly.CalculateVolume();
        }

        private void OnDrawGizmos()
        {
            if (vertices != null)
                foreach (var vertex in vertices)
                    Gizmos.DrawSphere(transform.TransformPoint(vertex.position), 0.025f);

            if (edges != null)
                foreach (var set in edges)
                    foreach (var edge in set)
                        Gizmos.DrawLine(transform.TransformPoint(edge.a.position), transform.TransformPoint(edge.b.position));
        }
    }
}