namespace CarbonFox.Volumetrics
{
    using UnityEngine;

    public class Tetrahedron : Polyhedron
    {
        public Tetrahedron(Vertex a, Vertex b, Vertex c, Vertex d)
        {
            vertices = new Vertex[] { a, b, c, d };
        }

        public override float CalculateVolume()
        {
            return ScalarTripleProductVolume();
        }

        /// <summary>
        /// Calculate volume using vector mathematics.
        /// </summary>
        private float ScalarTripleProductVolume()
        {
            // The volume of a tetrahedron is one-sixth of the scalar triple product of the vectors representing any three co-terminal edges of the tetrahedron.

            Vector3 x = vertices[1].position - vertices[0].position;
            Vector3 y = vertices[2].position - vertices[0].position;
            Vector3 z = vertices[3].position - vertices[0].position;

            return Vector3.Dot(Vector3.Cross(x, y), z) / 2f;
        }

        /// <summary>
        /// Alternative (deprecated) method for calculating volume without vectors.
        /// </summary>
        private float PyramidVolume()
        {
            // Volume of a triangular pyramid:
            // V = 1/3 * b * h

            float b = HeronsFormula.CalculateArea(vertices[0], vertices[1], vertices[2]);
            float h = CalculateHeight(vertices[0], vertices[1], vertices[2], vertices[3]);

            return (1f / 3f) * b * h;
        }

        private float CalculateHeight(Vertex a, Vertex b, Vertex c, Vertex d)
        {
            Plane plane = new Plane(a.position, b.position, c.position);
            float distance = plane.GetDistanceToPoint(d.position);
            float height = Mathf.Abs(distance);
            return height;
        }
    }
}