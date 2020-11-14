using UnityEngine;

namespace CarbonFox.Volumetrics
{
    [System.Serializable]
    public class Vertex
    {
        public Vector3 position;

        public Vertex (Vector3 position)
        {
            this.position = position;
        }
    }
}