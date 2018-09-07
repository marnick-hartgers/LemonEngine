using LemonEngine.RenderLogic.Shaders.Programs;
using SharpGL;
using SharpGL.SceneGraph.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.RenderLogic.Shaders
{
    public class FrameBuffer
    {
        private uint _colorRenderBufferHandle;
        private uint _depthRenderBufferHandle;
        private uint _frameBufferHandle;
        private uint[] _screenTexture = new uint[1];
        private uint _oldFrameBuffer;
        private FrameBufferTextureProgram _shader = new FrameBufferTextureProgram();

        private VertexBufferArray _buffers = new VertexBufferArray();
        private VertexBuffer _quadVertexBuffer = new VertexBuffer();
        private VertexBuffer _quadTexBuffer = new VertexBuffer();

        private int width = 1924, height = 1024;

        public void Init(OpenGL gl)
        {
            gl.GetError();
            int[] oldFbo = new int[1];
            gl.GetInteger(OpenGL.GL_FRAMEBUFFER_BINDING_EXT, oldFbo);
            _oldFrameBuffer = (uint)oldFbo[0];
            uint[] ids = new uint[1];
            //Gen FrameBuffer
            gl.GenFramebuffersEXT(1, ids);
            _frameBufferHandle = ids[0];
            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _frameBufferHandle);


            //Gen ColorRenderBuffer
            gl.GenRenderbuffersEXT(1, ids);
            _colorRenderBufferHandle = ids[0];
            gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER_EXT, _colorRenderBufferHandle);
            gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER_EXT, OpenGL.GL_RGBA, width, height); // use a single renderbuffer object for both a depth AND stencil buffer.

            //Gen DepthRenderBuffer
            gl.GenRenderbuffersEXT(1, ids);
            _depthRenderBufferHandle = ids[0];
            gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER_EXT, _depthRenderBufferHandle);
            gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER_EXT, OpenGL.GL_DEPTH_COMPONENT24, width, height); // use a single renderbuffer object for both a depth AND stencil buffer.

            //Link depth and color buffer
            gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_RENDERBUFFER_EXT, _colorRenderBufferHandle);
            gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, _depthRenderBufferHandle);

            //Gen Texture
            gl.GenTextures(1, _screenTexture);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, _screenTexture[0]);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGB, width, height, 0, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, null);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);


            gl.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_TEXTURE_2D, _screenTexture[0], 0);

            uint status = gl.CheckFramebufferStatusEXT(_frameBufferHandle);
            if (status != OpenGL.GL_FRAMEBUFFER_COMPLETE_EXT)
            {
                Console.WriteLine("ERR: " + gl.GetErrorDescription(gl.GetError()));
                string err = gl.GetErrorDescription(status);
                //throw new Exception("FBO STUK");
            }

            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _oldFrameBuffer);
            InitQuad(gl);
            _shader.Init(gl);
        }

        public void BeginRender(OpenGL gl)
        {
            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _frameBufferHandle);
        }

        public void EndRender(OpenGL gl)
        {
            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _oldFrameBuffer);
            
            gl.ClearColor(0.0f, 1f, 0.0f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, _screenTexture[0]);
            _shader.Bind(gl, _screenTexture[0]);
            DrawQuad(gl);
            _shader.Unbind(gl);

        }

        private void InitQuad(OpenGL gl)
        {
            _buffers.Create(gl);
            _buffers.Bind(gl);
            _quadVertexBuffer.Create(gl);
            _quadVertexBuffer.Bind(gl);
            _quadVertexBuffer.SetData(gl, 0, new float[] {
                -1.0f,  1.0f, 0.0f,
                -1.0f, -1.0f, 0.0f,
                 1.0f, -1.0f, 0.0f,

                -1.0f,  1.0f, 0.0f,
                 1.0f, -1.0f, 0.0f,
                 1.0f,  1.0f, 0.0f,
            }, false, 3);

            _quadTexBuffer.Create(gl);
            _quadTexBuffer.Bind(gl);
            _quadTexBuffer.SetData(gl, 1, new float[] {
                0.0f, 1.0f,
                0.0f, 0.0f,
                1.0f, 0.0f,

                0.0f, 1.0f,
                1.0f, 0.0f,
                1.0f, 1.0f
            }, false, 2);

            _buffers.Unbind(gl);
        }

        private void DrawQuad(OpenGL gl)
        {
            _buffers.Bind(gl);
            
            //gl.Color(1f, 1f, 1f);
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 18);
            _buffers.Unbind(gl);
        }
    }
}
