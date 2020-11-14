namespace CarbonFox.Volumetrics
{
    public abstract class Polyhedron
    {
        public Vertex[] vertices;

        public abstract float CalculateVolume();
    }
}