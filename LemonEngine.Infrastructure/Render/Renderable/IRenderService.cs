using SharpGL;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Infrastructure.Render.Settings;
using LemonEngine.Infrastructure.Logic.Output;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    public interface IRenderService
    {
        void Init(OpenGL gl);
        void Render(OpenGL gl);
        void SetRenderSettings(RenderSettings renderSettings);
        void ReceiveOutput(LogicOutputContainer output);
    }
}
