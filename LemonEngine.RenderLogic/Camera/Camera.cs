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
        public Camera(RenderService renderService)
        {
            Position = new Vec3();
            Rotation = new Vec3();
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, 1920.0f / 1024.0f, 0.01f, 1000.0f);
        }

        public Vec3 Position { get; }
        public Vec3 Rotation { get; }

        public void SetParameters(Vec3 position, Vec3 rotation)
        {
            Vec3.Copy(position, Position);
            Vec3.Copy(rotation, Rotation);
        }

        private mat4 projectionMatrix;
        private mat4 viewMatrix;

        public void SetCamera(OpenGL gl, IShader shader)
        {
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(Position.X, Position.Y, Position.Z));
            viewMatrix = glm.rotate(viewMatrix, Rotation.X, new vec3(1, 0, 0));
            viewMatrix = glm.rotate(viewMatrix, Rotation.Y, new vec3(0, 1, 0));
            viewMatrix = glm.rotate(viewMatrix, Rotation.Z, new vec3(0, 0, 1));
            shader.ShaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.ShaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());

        }
    }
}
