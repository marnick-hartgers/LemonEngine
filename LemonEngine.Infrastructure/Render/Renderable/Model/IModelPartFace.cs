using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelPartFace
    {
        Int4 Vertex { get; set; }
        Int4 VertexTexture { get; set; }
        Int4 VertexNormal { get; set; }

        DrawType DrawType { get; set; }
    }

    public enum DrawType
    {
        Triangle,
        TriangleTexture,
        TriangleTextureNormal,
        Quad,
        QuadTexture,
        QuadTextureNormal,
    }
}
