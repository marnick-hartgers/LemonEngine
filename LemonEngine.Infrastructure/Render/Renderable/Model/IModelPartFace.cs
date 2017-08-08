using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelPartFace
    {
        int[] Vertex { get; set; }
        int[] VertexTexture { get; set; }
        int[] VertexNormal { get; set; }
        bool HasVertexTexture { get; set; }
        bool HasVertexNormal { get; set; }
        IMaterial Material { get; set; }
    }
}
