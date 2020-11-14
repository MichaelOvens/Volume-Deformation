namespace CarbonFox.Volumetrics
{
    [System.Serializable]
    public class Hexahedron : Polyhedron
    {
        public Tetrahedron[] tetrahedrons;

        /*
         _______________________
        |                       |
        |                       |
        |        f--------g     |
        |       /|       /|     |
        |      / |      / |     |
        |     e--|-----h  |     |
        |     |  |     |  |     |       Vertex assignments
        |     |  b-----|--c     |
        |     | /      | /      |
        |     |/       |/       |
        |     a--------d        |
        |                       |
        |_______________________|

        */

        public Hexahedron(Vertex a, Vertex b, Vertex c, Vertex d, Vertex e, Vertex f, Vertex g, Vertex h)
        {
            tetrahedrons = new Tetrahedron[]
            {
                new Tetrahedron(a, b, d, e),
                new Tetrahedron(b, c, d, g),
                new Tetrahedron(e, f, g, b),
                new Tetrahedron(g, h, e, d),
                new Tetrahedron(b, d, e, g)
            };
        }

        public override float CalculateVolume()
        {
            float volume = 0f;
            
            foreach (var tetra in tetrahedrons)
                if (tetra != null)
                    volume += tetra.CalculateVolume();

            return volume;
        }
    }
}