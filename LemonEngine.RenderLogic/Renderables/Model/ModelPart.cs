using System.Collections.Generic;
using GlmNet;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Shaders;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.VertexBuffers;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class ModelPart : IModelPart
    {
        private VertexBufferArray _buffers = new VertexBufferArray();
        private VertexBuffer _vertexBuffer = new VertexBuffer();
        private VertexBuffer _ambColorBuffer = new VertexBuffer();
        private VertexBuffer _difColorBuffer = new VertexBuffer();
        private VertexBuffer _speColorBuffer = new VertexBuffer();
        private VertexBuffer _normalBuffer = new VertexBuffer();
        //private VertexBuffer _normalBuffer = new VertexBuffer();
        public int VertexCount => _vertexData.Length;



        //private VertexBuffer _texcordBuffer = new VertexBuffer();
        public ModelPart(string name, IMaterial material, float[] vertexData, float[] amColorData, float[] difColorData, float[] specColorData, float[] normalData)
        {
            Name = name;
            _material = material;
            _vertexData = vertexData;
            _normalData = normalData;
            _ambColorData = amColorData;
            _difColorData = difColorData;
            _speColorData = specColorData;
        }
        public string Name { get; }
        private IMaterial _material { get; }
        private float[] _vertexData { get; }
        private float[] _normalData { get; }
        private float[] _ambColorData { get; }
        private float[] _difColorData { get; }
        private float[] _speColorData { get; }



        public void BindToGl(OpenGL gl, IShader shader)
        {
            _buffers.Create(gl);
            _buffers.Bind(gl);
            _vertexBuffer.Create(gl);
            _vertexBuffer.Bind(gl);
            _vertexBuffer.SetData(gl, shader.PositionAttributeIndex, _vertexData, false, 3);

            _ambColorBuffer.Create(gl);
            _ambColorBuffer.Bind(gl);
            _ambColorBuffer.SetData(gl, shader.AmbColorAttributeIndex, _ambColorData, false, 3);

            _difColorBuffer.Create(gl);
            _difColorBuffer.Bind(gl);
            _difColorBuffer.SetData(gl, shader.DifColorAttributeIndex, _difColorData, false, 3);

            _speColorBuffer.Create(gl);
            _speColorBuffer.Bind(gl);
            _speColorBuffer.SetData(gl, shader.SpeColorAttributeIndex, _speColorData, false, 3);

            if (_normalData.Length > 0)
            {
                _normalBuffer.Create(gl);
                _normalBuffer.Bind(gl);
                _normalBuffer.SetData(gl, shader.NormalAttributeIndex, _normalData, false, 3);
            }
            _buffers.Unbind(gl);
        }

        public void BindForDraw(OpenGL gl, IShader shader)
        {
            _buffers.Bind(gl);
        }

        public void UnbindForDraw(OpenGL gl, IShader shader)
        {
            _buffers.Unbind(gl);
        }
    }
}