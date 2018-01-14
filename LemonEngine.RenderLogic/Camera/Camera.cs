using System;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Shaders;
using SharpGL;
using GlmNet;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Shader;

namespace LemonEngine.RenderLogic.Camera
{
    public class Camera : ICamera
    {
        public Camera()
        {
            Position = new Vec3();
            Rotation = new Vec3();
            const float rads = (60.0f / 360.0f) * (float) Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, 800.0f / 600.0f, 0.01f, 1000.0f);
            //projectionMatrix = glm.scale(projectionMatrix, new vec3(-1));
        }

        public Vec3 Position { get; }
        public Vec3 Rotation { get; }

        private mat4 projectionMatrix;
        private mat4 viewMatrix;

        public void SetCamera(OpenGL gl, IShader shader)
        {
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(Position.X, Position.Y, Position.Z));
            shader.ShaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.ShaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            
        }
    }
}
