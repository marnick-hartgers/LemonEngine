using System;
using System.Collections.Generic;
using System.Linq;
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


        public void DrawPart(IModelPart part, OpenGL gl)
        {
            var firstface = part.Faces.FirstOrDefault();
            if (firstface == null)
            {
                return;
            }
            var type = firstface.DrawType;
            if (type == DrawType.Triangle || type == DrawType.TriangleTexture || type == DrawType.TriangleTextureNormal)
            {
                DrawWithTriangles(part, gl);
            }
            else
            {
                DrawWithQuads(part, gl);
            }
        }


        private void DrawWithTriangles(IModelPart part, OpenGL gl)
        {
            gl.Begin(BeginMode.Triangles);
            
            for (int d = 0; d < part.Faces.Count; d++)
            {
                gl.Color((float)d * d % 255 / 255, 0, 255f -(float)d * d % 255 / 255);

                if (part.Faces[d].DrawType == DrawType.TriangleTexture)
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Three].AsArray);
                }
                else if (part.Faces[d].DrawType == DrawType.TriangleTextureNormal)
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.One].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Two].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Three].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.Three].AsArray);
                }
                else
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);
                }

            }
            gl.End();
        }

        private void DrawWithQuads(IModelPart part, OpenGL gl)
        {
            return;//not supported
            gl.Begin(BeginMode.Quads);
            for (int d = 0; d < part.Faces.Count; d++)
            {


                if (part.Faces[d].DrawType == DrawType.QuadTexture)
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Three].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Four].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Four].AsArray);
                }
                else if (part.Faces[d].DrawType == DrawType.QuadTextureNormal)
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.One].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Two].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Three].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.Three].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Four].AsArray);
                    gl.TexCoord(Vertexs[part.Faces[d].VertexTexture.Four].AsArray);
                    gl.Normal(Vertexs[part.Faces[d].VertexNormal.Four].AsArray);
                }
                else
                {
                    gl.Vertex(Vertexs[part.Faces[d].Vertex.One].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Two].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Three].AsArray);

                    gl.Vertex(Vertexs[part.Faces[d].Vertex.Four].AsArray);
                }

            }
            gl.End();
        }

    }
}