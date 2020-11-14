namespace CarbonFox.Volumetrics
{
    using UnityEngine;

    public static class HeronsFormula
    {
        public static float CalculateArea(Vertex a, Vertex b, Vertex c)
        {
            float ab = Vector3.Distance(a.position, b.position);
            float bc = Vector3.Distance(b.position, c.position);
            float ca = Vector3.Distance(c.position, a.position);

            float area = CalculateArea(ab, bc, ca);

            return area;
        }

        public static float CalculateArea(float ab, float bc, float ca)
        {
            float s = (ab + bc + ca) / 2f;

            float area = Mathf.Sqrt(s * (s - ab) * (s - bc) * (s - ca));
            return area;
        }
    }
}