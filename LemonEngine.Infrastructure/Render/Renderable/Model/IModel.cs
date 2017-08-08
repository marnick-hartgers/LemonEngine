using System.Collections.Generic;
using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModel
    {
        string Name { get; set; }

        string MaterialGroup { get; set; }

        List<IModelPart> Parts { get; }

        List<Vec3> Vertexs { get; }
        List<Vec3> VertexsTextures { get; }
        List<Vec3> VertexsNormal { get; }

        void DrawPart(IModelPart part, IMaterialGroup materialGroup, OpenGL gl);
    }
}
