using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public static class Tools
    {
        public static List<Mesh> CreateNewCustomMesh(RebarGroup rebarGroup, int numberOfSegments, int accuracy)
        {
            List<Mesh> newRebarMeshes = new List<Mesh>();
            double rebarGroupRadius = rebarGroup.Diameter / 2.0;
            
            foreach (var curve in rebarGroup.RebarGroupCurves)
            {
                newRebarMeshes.Add(Mesh.CreateFromCurvePipe(curve, rebarGroupRadius, numberOfSegments, accuracy, MeshPipeCapStyle.Flat, false));
            }

            return newRebarMeshes;
        }
    }
}