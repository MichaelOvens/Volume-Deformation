using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarbonFox.Volumetrics 
{
    public static class Volume3DFactory
    {
        public static void Populate(Volume3D volume)
        {
            CalculateCount(volume);
            CreateVertices(volume);
            CreateEdges(volume);
            CreateHexahedrons(volume);
        }

        private static void CalculateCount (Volume3D volume)
        {
            volume.count = new Vector3Int()
            {
                x = (int)(volume.size.x * volume.density.x) + 1,
                y = (int)(volume.size.y * volume.density.y) + 1,
                z = (int)(volume.size.z * volume.density.z) + 1
            };
        }

        private static void CreateVertices (Volume3D volume)
        {
            Vector3 spacing = new Vector3()
            {
                x = 1f / (volume.density.x),
                y = 1f / (volume.density.y),
                z = 1f / (volume.density.z)
            };

            Vector3 origin = new Vector3()
            {
                x = (-volume.size.x / 2f),
                y = (-volume.size.y / 2f),
                z = (-volume.size.z / 2f)
            };

            volume.vertices = new Vertex[volume.count.x, volume.count.y, volume.count.z];

            for (int x = 0; x < volume.count.x; x++)
            {
                for (int y = 0; y < volume.count.y; y++)
                {
                    for (int z = 0; z < volume.count.z; z++)
                    {
                        Vector3 position = new Vector3()
                        {
                            x = origin.x + (spacing.x * x),
                            y = origin.y + (spacing.y * y),
                            z = origin.z + (spacing.z * z)
                        };

                        volume.vertices[x, y, z] = new Vertex(position);
                    }
                }
            }
        }

        private static void CreateEdges (Volume3D volume)
        {
            volume.edges = new Edge[volume.count.x,volume.count.y,volume.count.z][];

            var buffer = new List<Edge>(6);

            for (int x = 0; x < volume.count.x; x++)
            {
                for (int y = 0; y < volume.count.y; y++)
                {
                    for (int z = 0; z < volume.count.z; z++)
                    {
                        if ((x + 1) < volume.count.x)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x + 1, y, z]));

                        if ((x - 1) >= 0)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x - 1, y, z]));

                        if ((y + 1) < volume.count.y)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x, y + 1, z]));

                        if ((y - 1) >= 0)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x, y - 1, z]));

                        if ((z + 1) < volume.count.z)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x, y, z + 1]));

                        if ((z - 1) >= 0)
                            buffer.Add(new Edge(volume.vertices[x, y, z], volume.vertices[x, y, z - 1]));

                        volume.edges[x, y, z] = buffer.ToArray();
                        buffer.Clear();
                    }
                }
            }
        }

        private static void CreateHexahedrons (Volume3D volume)
        {
            List<Hexahedron> hexahedrons = new List<Hexahedron>();

            for (int x = 0; x < volume.count.x; x++)
            {
                for (int y = 0; y < volume.count.y; y++)
                {
                    for (int z = 0; z < volume.count.z; z++)
                    {
                        bool withinX = (x + 1) < volume.count.x;
                        bool withinY = (y + 1) < volume.count.y;
                        bool withinZ = (z + 1) < volume.count.z;

                        if (withinX && withinY && withinZ)
                        {
                            Vertex a = volume.vertices[x, y, z];
                            Vertex b = volume.vertices[x, y, z + 1];
                            Vertex c = volume.vertices[x + 1, y, z + 1];
                            Vertex d = volume.vertices[x + 1, y, z];
                            Vertex e = volume.vertices[x, y + 1, z];
                            Vertex f = volume.vertices[x, y + 1, z + 1];
                            Vertex g = volume.vertices[x + 1, y + 1, z + 1];
                            Vertex h = volume.vertices[x + 1, y + 1, z];

                            hexahedrons.Add(new Hexahedron(a, b, c, d, e, f, g, h));
                        }
                    }
                }
            }

            volume.polyhedrons = hexahedrons.ToArray();

            Debug.Log(string.Format("Created {0} hexahedrons", hexahedrons.Count));
        }
    }
}