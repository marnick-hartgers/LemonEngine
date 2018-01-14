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
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class Model : IModel
    {
        public string Name { get; set; }
        public string MaterialGroup { get; set; }
        
        public List<IModelPart> Parts { get; }
        private Vec3 _position;
        private Vec3 _rotation;
        private Vec3 _scale;

        private IShader _shader;

        private mat4 modelMatrix;

        public Model()
        {
            Parts = new List<IModelPart>();
            _shader = new DefaultShader();

            _position = new Vec3();
            _rotation = new Vec3();
            _scale = new Vec3();
                modelMatrix = new mat4(1);

        }
        

        public void Draw(OpenGL gl, ICamera camera)
        {
            _shader.BindToGl(gl);
            camera.SetCamera(gl, _shader);
            _shader.ShaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            Parts.Reverse();
            
            foreach (IModelPart part in Parts)
            {
                part.BindForDraw(gl, _shader);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, part.VertexCount);
                part.UnbindForDraw(gl, _shader);
            }
            _shader.UnbindToGl(gl);
        }

        public void SetRotation(Vec3 rotation)
        {
            modelMatrix = glm.rotate(modelMatrix, _rotation.X - rotation.X, new vec3(1, 0, 0));
            modelMatrix = glm.rotate(modelMatrix, _rotation.Y - rotation.Y, new vec3(0, 1, 0));
            modelMatrix = glm.rotate(modelMatrix, _rotation.Z - rotation.Z, new vec3(0, 0, 1));
            _rotation.X = rotation.X;
            _rotation.Y = rotation.Y;
            _rotation.Z = rotation.Z;
        }

        private void SetMaterial(OpenGL gl, IMaterial material)
        {
            material.Set(gl);
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
