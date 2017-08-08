using System;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Render.Renderable;
using SharpGL;
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic
{

    public class RenderEnigne
    {
        public EventHandler OnLoadDone;

        private RenderService _renderService;

        private RenderTools _renderTools;

        private RenderSettings _renderSettings;
        public void StartLoad(OpenGL gl)
        {
            _renderSettings = new RenderSettings();
            
            //gl.Enable(OpenGL.GL_BLEND);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            SetRenderSettings(gl);
            //gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            
            gl.ClearColor(0.5f,0.7f,1f,0f);
            _renderService = new RenderService();
            _renderService.Init(gl);


            _renderTools = new RenderTools();
            _renderTools.LightChanged += SetLightChanged;
            _renderTools.SmoothChanged += SetSmoothChanged;
            _renderTools.ColorMaterialChanged += SetColorMaterialChanged;
            _renderTools.TexturesChanged += SetTexturesChanged;
            _renderTools.Show();


            SignalLoadDone();

        }

        private void SetColorMaterialChanged(object sender, ToggleEventArgs toggleEventArgs)
        {
            _renderSettings.UseColorMaterials = toggleEventArgs.State;
            _renderSettings.HasPendingChanges = true;
        }

        private void SetSmoothChanged(object sender, ToggleEventArgs toggleEventArgs)
        {
            _renderSettings.UseSmooth = toggleEventArgs.State;
            _renderSettings.HasPendingChanges = true;
        }

        private void SetLightChanged(object sender, ToggleEventArgs toggleEventArgs)
        {
            _renderSettings.UseLight = toggleEventArgs.State;
            _renderSettings.HasPendingChanges = true;
        }
        private void SetTexturesChanged(object sender, ToggleEventArgs toggleEventArgs)
        {
            _renderSettings.UseTextures = toggleEventArgs.State;
            _renderSettings.HasPendingChanges = true;
        }

        private void SignalLoadDone()
        {
            OnLoadDone?.Invoke(this, EventArgs.Empty);
        }

        public IRenderable AddRenderable(string model, string material)
        {
            return _renderService.AddRenderable(model, material);
        }

        public void Render(OpenGL gl)
        {
            if (_renderSettings.HasPendingChanges)
            {
                SetRenderSettings(gl);
            }
            _renderService.Render(gl);
        }

        public void AddLight(ILight light)
        {
            _renderService.AddLight(light);
        }

        private void SetRenderSettings(OpenGL gl)
        {
            if (_renderSettings.UseLight)
            {
                gl.Enable(OpenGL.GL_LIGHTING);
            }
            else
            {
                gl.Disable(OpenGL.GL_LIGHTING);
            }
            if (_renderSettings.UseSmooth)
            {
                gl.ShadeModel(ShadeModel.Smooth);
            }
            else
            {
                gl.ShadeModel(ShadeModel.Flat);
            }
            if (_renderSettings.UseColorMaterials)
            {
                gl.Enable(OpenGL.GL_COLOR_MATERIAL);
                gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE);
            }
            else
            {
                gl.Disable(OpenGL.GL_COLOR_MATERIAL);
            }
            if (_renderSettings.UseTextures)
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
            }
            else
            {
                gl.Disable(OpenGL.GL_TEXTURE_2D);
            }

            _renderSettings.HasPendingChanges = false;
        }
    }

    public class RenderSettings
    {
        public bool UseLight = true;
        public bool UseSmooth = true;
        public bool UseColorMaterials = false;
        public bool UseTextures = true;
        public bool HasPendingChanges = false;
    }
}
