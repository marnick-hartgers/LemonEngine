using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class ModelPartFace : IModelPartFace
    {
        public Int4 Vertex { get; set; }
        public Int4 VertexTexture { get; set; }
        public Int4 VertexNormal { get; set; }
        public DrawType DrawType { get; set; }
    }
}