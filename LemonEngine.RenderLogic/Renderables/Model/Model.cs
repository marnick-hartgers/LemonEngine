using System;
using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using SharpGL;
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class Model : IModel
    {
        public string Name { get; set; }

        public string MaterialGroup { get; set; }

        public List<IModelPart> Parts { get; }

        public List<Vec3> Vertexs { get; }
        public List<Vec3> VertexsTextures { get; }
        public List<Vec3> VertexsNormal { get; }

        public Model()
        {
            Parts = new List<IModelPart>();
            Vertexs = new List<Vec3>();
            VertexsTextures = new List<Vec3>();
            VertexsNormal = new List<Vec3>();
        }

        private int numVertexes = 0;
        private int _partIndex = 0;
        private IModelPartFace _currentFace = null;

        public void DrawPart(IModelPart part, IMaterialGroup materialGroup, OpenGL gl)
        {
            
            for ( _partIndex = 0; _partIndex < part.Faces.Count; _partIndex++)
            {
                _currentFace = part.Faces[_partIndex];
                SetMaterial(gl, _currentFace.Material);
                numVertexes = _currentFace.Vertex.Length;
                SetBeginMode(gl, numVertexes);
                Draw(_currentFace, gl);
                gl.End();
                UnsetMaterial(gl, _currentFace.Material);
            }
        }

        private void SetBeginMode(OpenGL gl, int numVertexes)
        {
            switch (numVertexes)
            {
                case 1:
                    gl.Begin(BeginMode.Points);
                    break;
                case 2:
                    gl.Begin(BeginMode.Lines);
                    break;
                case 3:
                    gl.Begin(BeginMode.Triangles);
                    break;
                case 4:
                    gl.Begin(BeginMode.Quads);
                    break;
                default:
                    throw new IndexOutOfRangeException("No polygons please");
                    break;

            }
        }

        private void SetMaterial(OpenGL gl, IMaterial material)
        {
            material.Set(gl);
        }

        private void UnsetMaterial(OpenGL gl, IMaterial material)
        {
            material.Unset(gl);
        }

        private int _drawIndex = 0;
        private void Draw(IModelPartFace part, OpenGL gl)
        {
            for (_drawIndex = 0; _drawIndex < part.Vertex.Length; _drawIndex++)
            {
                gl.Vertex(Vertexs[part.Vertex[_drawIndex]].AsArray);
                if (part.HasVertexTexture)
                {
                    gl.TexCoord(VertexsTextures[part.VertexTexture[_drawIndex]].AsArray);
                }
                if (part.HasVertexNormal)
                {
                    gl.Normal(VertexsNormal[part.VertexNormal[_drawIndex]].AsArray);
                }

            }

        }


    }
}
