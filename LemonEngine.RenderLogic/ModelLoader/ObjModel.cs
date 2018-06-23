using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Model;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjModel
    {
        public ObjModel()
        {
            Vertexs = new List<Vec3>();
            Normals = new List<Vec3>();
            TextCords = new List<Vec3>();
            Parts = new List<ObjModelPart>();
        }

        public string Name { get; set; }
        public string MaterialGroup { get; set; }
        public List<Vec3> Vertexs { get; }
        public List<Vec3> Normals { get; }
        public List<Vec3> TextCords { get; }
        public List<ObjModelPart> Parts { get; }

        public Model ToModel()
        {
            Model model = new Model();
            model.Name = Name;
            foreach (ObjModelPart objModelPart in Parts)
            {
                var p = MakePart(objModelPart);
                if (p != null)
                {
                    model.Parts.Add(p);

                }
            }
            return model;
        }

        private Vec3 normalizise(Vec3 inValue)
        {
            float max = Math.Max(inValue.X, Math.Max(inValue.Y, inValue.Z));
            //return new Vec3(inValue.X / max, inValue.Y / max, inValue.Z / max);
            return new Vec3(0,1,0);
        }

        public ModelPart MakePart(ObjModelPart part)
        {
            if (part.FaceVertexes.Count == 0)
            {
                return null;
            }
            List<float> vertexData = new List<float>();
            List<float> normalData = new List<float>();
            List<float> textCordData = new List<float>();
            List<float> amColorData = new List<float>();
            List<float> defColorData = new List<float>();
            List<float> secColorData = new List<float>();
            for (int i = 0; i < part.FaceVertexes.Count; i++)
            {
                var vertex = Vertexs[part.FaceVertexes[i]];
                
                vertexData.Add(vertex.X);
                vertexData.Add(vertex.Y);
                vertexData.Add(vertex.Z);

                amColorData.AddRange(part.Material.AmbColor.AsArray);
                defColorData.AddRange(part.Material.DifColor.AsArray);
                secColorData.AddRange(part.Material.SpeColor.AsArray);

                if (i < part.FaceNormals.Count)
                {
                    var normal = Normals[part.FaceNormals[i]];
                    normalData.Add(normal.X);
                    normalData.Add(normal.Y);
                    normalData.Add(normal.Z);
                }
                else
                {
                    var normal = normalizise(vertex);
                    normalData.Add(normal.X);
                    normalData.Add(normal.Y);
                    normalData.Add(normal.Z);
                }


                if (i < part.FaceTextCords.Count)
                {
                    var textCord = TextCords[part.FaceTextCords[i]];
                    textCordData.Add(textCord.X);
                    textCordData.Add(textCord.Y);
                }
                else
                {
                    textCordData.Add(0);
                    textCordData.Add(0);
                }
                

            }
            return new ModelPart(part.Name, part.Material, vertexData.ToArray(), amColorData.ToArray(), defColorData.ToArray(), secColorData.ToArray(), normalData.ToArray(), textCordData.ToArray());
        }
    }
}
