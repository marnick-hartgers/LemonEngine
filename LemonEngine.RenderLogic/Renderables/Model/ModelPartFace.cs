using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class ModelPartFace : IModelPartFace
    {
        public int[] Vertex { get; set; }
        public int[] VertexTexture { get; set; }
        public int[] VertexNormal { get; set; }
        public bool HasVertexTexture { get; set; }
        public bool HasVertexNormal { get; set; }
        public IMaterial Material { get; set; }
    }
}