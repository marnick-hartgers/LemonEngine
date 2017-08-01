using System.Collections.Generic;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Infrastructure.Types.Render;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjModelPart
    {
        public string Name { get; set; }
        public string MaterialName { get; set; }

        public List<Vec3> Vertexs = new List<Vec3>();
        public List<Vec3> VertexsTextures = new List<Vec3>();
        public List<Vec3> VertexsNormal = new List<Vec3>();
        public List<ObjModelPartFace> Faces = new List<ObjModelPartFace>();

    }
}