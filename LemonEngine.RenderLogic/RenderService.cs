using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables;
using LemonEngine.RenderLogic.Renderables.Material;
using LemonEngine.RenderLogic.Renderables.Model;
using LemonEngine.RenderLogic.Shaders;
using LemonEngine.RenderLogic.Camera;
using SharpGL;
using LemonEngine.Infrastructure.Render.Settings;
using LemonEngine.Infrastructure.Logic.Output;
using System;

namespace LemonEngine.RenderLogic
{
    public class RenderService : IRenderService
    {

        private readonly List<Renderable> _renderables = new List<Renderable>();
        private IModelRepository _modelRepository;
        private IMaterialRepository _materialRepository;
        private int _renderIndex = 0;

        private RenderSettings _renderSettings;
        private LogicOutputContainer _lastOutput;

        internal void SetAspectRatio(float x, float y)
        {
            Camera.SetAspectRatio(x,y);
        }

        public ICamera Camera { get; private set; }
        public Vec3 SkyColor { get; set; }


        public void Init(OpenGL gl)
        {
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Enable(OpenGL.GL_BLEND);
            _materialRepository = MaterialRepository.GetInstance();
            _materialRepository.Load(gl);
            _modelRepository = ModelRepository.GetInstance();
            _modelRepository.StartLoad(_materialRepository);
            _modelRepository.BindAll(gl);
            Camera = new Camera.Camera(this);
            Camera.Position.X = -5;
            Camera.Position.Y = -4;
            Camera.Position.Z = -14;
            SkyColor = new Vec3(1,1,1);

            foreach (var materialGroup in _materialRepository.MaterialGroups)
            {
                foreach (var material in materialGroup.Materials)
                {
                    material.Init(gl);
                }
            }
        }

        private IRenderable AddRenderable(string model, Guid id )
        {
            var r = new Renderable(model, id);
            _renderables.Add(r);
            return r;
        }


        public void Render(OpenGL gl)
        {
            UpdateFromOutput();
            RenderSettings renderSettings = _renderSettings ?? RenderSettings.Empty;
            gl.ClearColor(renderSettings.ClearColor.X, renderSettings.ClearColor.Y, renderSettings.ClearColor.Z, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            _renderIndex = 0;
            while (_renderIndex < _renderables.Count)
            {
                _renderables[_renderIndex].DrawEntity(gl, Camera, renderSettings);
                _renderIndex++;
            }
            
            gl.Flush();
        }

        public void SetRenderSettings(RenderSettings renderSettings)
        {
            _renderSettings = renderSettings;
        }

        private void UpdateFromOutput()
        {
            LogicOutputContainer logicOutputContainer = _lastOutput;
            var rendableDefs = logicOutputContainer.GetRenderbleDefenitions();
            foreach (RenderbleDefenition rDef in rendableDefs)
            {
                UpdateRendable(rDef);
            }
        }

        private void UpdateRendable(RenderbleDefenition rDef)
        {
            foreach (IRenderable rendable in _renderables)
            {
                if (rendable.Id.Equals(rDef.Id))
                {
                    rendable.SyncFromDefenition(rDef);
                    return;
                }
            }
            IRenderable r = AddRenderable(rDef.ModelName, rDef.Id);
            r.SyncFromDefenition(rDef);
        }

        public void ReceiveOutput(LogicOutputContainer output)
        {
            _lastOutput = output;
        }
    }
}
