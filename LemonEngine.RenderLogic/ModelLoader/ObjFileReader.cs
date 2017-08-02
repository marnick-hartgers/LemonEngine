using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Infrastructure.Types.Render;
using LemonEngine.RenderLogic.Renderables.Model;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjFileReader
    {
        private string resourceFolder = "";

        public List<Model> ReadFolder()
        {
            List<Model> models = new List<Model>();
            MakeFolder();
            var filesToRead = GetFiles();
            foreach (var filename in filesToRead)
            {
                models.Add(ReadFile(filename));
            }
            return models;
        }

        private void MakeFolder()
        {
            resourceFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LemonEngine", "Models");
            if (!Directory.Exists(resourceFolder))
            {
                Directory.CreateDirectory(resourceFolder);
            }
        }

        private List<string> GetFiles()
        {
            List<string> filenames = new List<string>();
            var filesInFolder = Directory.GetFiles(resourceFolder);
            foreach (var fileInFolder in filesInFolder)
            {
                if (Path.GetExtension(fileInFolder).ToUpper() == ".OBJ")
                {
                    filenames.Add(fileInFolder);
                }
            }

            return filenames;
        }

        private Model ReadFile(string filename)
        {
            var fileStream = new StreamReader(filename);
            Model model = new Model();
            model.Name = Path.GetFileNameWithoutExtension(filename);
            string line = "";
            ModelPart workingObject = null;
            while ((line = fileStream.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {

                    string command = line.Substring(0, line.IndexOf(' '));
                    string value = line.Substring(line.IndexOf(' ') + 1);
                    

                    switch (command.ToUpper())
                    {
                        case "O":
                            workingObject = SetWorkingObject(model, value);
                            break;
                        case "MTLLIB":
                            SetMaterialGroupName(model, value);
                            break;
                        case "USEMTL":
                            SetWorkingMaterial(workingObject, value);
                            break;
                        case "V":
                            AddVertex(model, value);
                            break;
                        case "VT":
                            AddVertexTexture(model, value);
                            break;
                        case "VN":
                            AddVertexNormal(model, value);
                            break;
                        case "F":
                            AddFace(workingObject, value);
                            break;
                        default:
                            //ignore 
                            break;
                    }
                }
            }

            return model;
        }

        private void AddFace(ModelPart workingObject, string value)
        {
            ModelPartFace face = new ModelPartFace();
            var values = ParseIntsFromString(value);
            if (values.Length == 3)
            {
                Int4 vertex = new Int4(values[0] - 1, values[1] - 1, values[2] - 1, -1);
                face.Vertex = vertex;
                face.DrawType = DrawType.Triangle;
            }
            else if(values.Length == 4)
            {
                Int4 vertex = new Int4(values[0] - 1, values[1] - 1, values[2] - 1, values[3] - 1);
                face.Vertex = vertex;
                face.DrawType = DrawType.Quad;
            }
            else if (values.Length == 6)
            {
                Int4 vertex = new Int4(values[0] - 1, values[2] - 1, values[4] - 1, -1);
                Int4 texcor = new Int4(values[1] - 1, values[3] - 1, values[5] - 1, -1);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
                face.DrawType = DrawType.TriangleTexture;
            }
            else if (values.Length == 8)
            {
                Int4 vertex = new Int4(values[0] - 1, values[2] - 1, values[4] - 1, values[6] - 1);
                Int4 texcor = new Int4(values[1] - 1, values[3] - 1, values[5] - 1, values[7] - 1);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
                face.DrawType = DrawType.QuadTexture;
            }
            else if (values.Length == 9)
            {
                Int4 vertex = new Int4(values[0] - 1, values[3] - 1, values[6] - 1, -1);
                Int4 texcor = new Int4(values[1] - 1, values[4] - 1, values[7] - 1, -1);
                Int4 normal = new Int4(values[2] - 1, values[5] - 1, values[8] - 1, -1);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
                face.VertexNormal = normal;
                face.DrawType = DrawType.TriangleTextureNormal;
            }
            else if (values.Length == 12)
            {
                Int4 vertex = new Int4(values[0] - 1, values[3] - 1, values[6] - 1, values[9] - 1);
                Int4 texcor = new Int4(values[1] - 1, values[4] - 1, values[7] - 1, values[10] - 1);
                Int4 normal = new Int4(values[2] - 1, values[5] - 1, values[8] - 1, values[11] - 1);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
                face.VertexNormal = normal;
                face.DrawType = DrawType.QuadTextureNormal;
            }
            else
            {
                throw new Exception("FUCKED");
            }
            workingObject.Faces.Add(face);
        }

        private void AddVertex(Model workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Vertexs.Add(vertex);
        }
        private void AddVertexTexture(Model workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length == 2)
            {
                Vec3 vertex = new Vec3(values[0], values[1], 0);
                workingObject.Vertexs.Add(vertex);
            }
            else if (values.Length == 2)
            {
                Vec3 vertex = new Vec3(values[0], values[1], values[2]);
                workingObject.Vertexs.Add(vertex);
            }

        }
        private void AddVertexNormal(Model workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Vertexs.Add(vertex);
        }

        private void SetWorkingMaterial(ModelPart workingObject, string value)
        {
            workingObject.MaterialName = value;
        }

        private void SetMaterialGroupName(Model model, string value)
        {
            model.MaterialGroup = value.Substring(0, value.IndexOf(".mtl", StringComparison.Ordinal));
        }

        private ModelPart SetWorkingObject(Model model, string name)
        {
            ModelPart part = new ModelPart();
            part.Name = name;
            model.Parts.Add(part);
            return part;
        }

        private float[] ParseFloatsFromString(string value)
        {
            value = value.Replace(".",",");
            string[] values = value.Split(new char[] { ' ', '/'});
            float[] result = new float[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                result[index] = float.Parse(values[index]);
            }
            return result;
        }
        private int[] ParseIntsFromString(string value)
        {
            string[] values = value.Split(new char[] { ' ', '/' });
            int[] result = new int[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                result[index] = int.Parse(values[index]);
            }
            return result;
        }
    }
}
