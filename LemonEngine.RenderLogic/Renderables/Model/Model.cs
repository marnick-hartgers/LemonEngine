using System;
using System.Collections.Generic;
using System.Linq;
using GlmNet;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Shaders;
using SharpGL;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class Model : IModel
    {
        public string Name { get; set; }
        public string MaterialGroup { get; set; }
        
        public List<IModelPart> Parts { get; }

        private IShader _shader;

        private mat4 _modelMatrix;

        public Model()
        {
            Parts = new List<IModelPart>();
            _shader = new DefaultShader();
            _modelMatrix = new mat4(1);
        }
        

        public void Draw(OpenGL gl, ICamera camera)
        {
            _shader.BindToGl(gl);
            camera.SetCamera(gl, _shader);
            _shader.ShaderProgram.SetUniformMatrix4(gl, "modelMatrix", _modelMatrix.to_array());
            //Parts.Reverse();
            
            foreach (IModelPart part in Parts)
            {
                part.BindForDraw(gl, _shader);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, part.VertexCount);
                part.UnbindForDraw(gl, _shader);
            }
            _shader.UnbindToGl(gl);
        }

        public void SetParameters(Vec3 position, Vec3 rotation)
        {
            _modelMatrix = new mat4(1);
            SetPosition(position);
            SetRotation(rotation);
            
        }

        private void SetRotation(Vec3 rotation)
        {
            _modelMatrix = glm.rotate(_modelMatrix, rotation.X, new vec3(1, 0, 0));
            _modelMatrix = glm.rotate(_modelMatrix, rotation.Y, new vec3(0, 1, 0));
            _modelMatrix = glm.rotate(_modelMatrix, rotation.Z, new vec3(0, 0, 1));
        }

        private void SetPosition(Vec3 position)
        {
            _modelMatrix = glm.translate(_modelMatrix, new vec3(position.X, position.Y , position.Z));
        }

        private void SetMaterial(OpenGL gl, IMaterial material)
        {
            material.Set(gl, _shader);
        }

        private void UnsetMaterial(OpenGL gl, IMaterial material)
        {
            material.Unset(gl);
        }

        public void BindToGl(OpenGL gl)
        {
            _shader.Create(gl);
            foreach (IModelPart part in Parts)
            {
                part.BindToGl(gl, _shader);
            }
        }
    }
}
