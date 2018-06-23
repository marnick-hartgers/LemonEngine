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
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, 1920.0f / 1024.0f, 0.01f, 1000.0f);
        }

        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
        public float FieldOfView
        {
            get => _fieldOfView;
            set
            {
                _fieldOfView = value;
            }
        }

        private float _fieldOfView = 60f;

        private Vec2 _aspectRatio = new Vec2(1920, 1024);

        public void SetParameters(Vec3 position, Vec3 rotation)
        {
            Vec3.Copy(position, Position);
            Vec3.Copy(rotation, Rotation);
        }

        public void SetAspectRatio(float x, float y)
        {
            Console.WriteLine($"Aspect ratio: {x}:{y}");
            _aspectRatio.X = x;
            _aspectRatio.Y = y;
        }

        private mat4 projectionMatrix;
        private mat4 viewMatrix;
        private mat4 viewMatrixRoration;

        public void SetCamera(OpenGL gl, IShader shader)
        {
            float rads = (_fieldOfView / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, _aspectRatio.X / _aspectRatio.Y, 0.01f, 100.0f);

            
            //if (Rotation.Max != 0 || Rotation.Min != 0)
            //{
            //    float angle = Rotation.Max;
            //    Vec3 normal = Rotation.GetNormal();
            //    viewMatrix = glm.rotate(viewMatrix, angle, new vec3(normal.X, normal.Y, normal.Z));
            //}
            viewMatrix = glm.rotate(new mat4(1.0f), Rotation.X, new vec3(1, 0, 0));
            viewMatrix = glm.rotate(viewMatrix, Rotation.Y, new vec3(0, 1, 0));
            viewMatrix = glm.rotate(viewMatrix, Rotation.Z, new vec3(0, 0, 1));
            viewMatrix = glm.translate(viewMatrix, new vec3(-Position.X, -Position.Y, -Position.Z));
            shader.ShaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.ShaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            //viewMatrixRoration = glm.rotate(new mat4(1.0f), Rotation.X, new vec3(1, 0, 0));
            viewMatrixRoration = glm.rotate(new mat4(1.0f), Rotation.Y, new vec3(0, 1, 0));
            //viewMatrixRoration = glm.rotate(viewMatrixRoration, Rotation.Z, new vec3(0, 0, 1));
            shader.ShaderProgram.SetUniformMatrix4(gl, "viewMatrixRotation", viewMatrixRoration.to_array());

        }
    }
}
