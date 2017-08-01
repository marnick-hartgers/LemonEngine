using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Infrastructure.Types.Render;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjFileReader
    {
        private string resourceFolder = "";

        public List<ObjModel> ReadFolder()
        {
            List<ObjModel> models = new List<ObjModel>();
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

        private ObjModel ReadFile(string filename)
        {
            var fileStream = new StreamReader(filename);
            ObjModel model = new ObjModel();
            model.Name = Path.GetFileNameWithoutExtension(filename);
            string line = "";
            ObjModelPart workingObject = null;
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
                            AddVertex(workingObject, value);
                            break;
                        case "VT":
                            AddVertexTexture(workingObject, value);
                            break;
                        case "VN":
                            AddVertexNormal(workingObject, value);
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

        private void AddFace(ObjModelPart workingObject, string value)
        {
            ObjModelPartFace face = new ObjModelPartFace();
            var values = ParseFloatsFromString(value);
            if (values.Length == 3)
            {
                Vec3 vertex = new Vec3(values[0], values[1], values[2]);
                face.Vertex = vertex;
            }else if (values.Length == 6)
            {
                Vec3 vertex = new Vec3(values[0], values[2], values[4]);
                Vec3 texcor = new Vec3(values[1], values[3], values[5]);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
            }
            else if (values.Length == 9)
            {
                Vec3 vertex = new Vec3(values[0], values[3], values[6]);
                Vec3 texcor = new Vec3(values[1], values[4], values[7]);
                Vec3 normal = new Vec3(values[2], values[5], values[8]);
                face.Vertex = vertex;
                face.VertexTexture = texcor;
                face.VertexNormal = normal;
            }
            workingObject.Faces.Add(face);
        }

        private void AddVertex(ObjModelPart workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Vertexs.Add(vertex);
        }
        private void AddVertexTexture(ObjModelPart workingObject, string value)
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
        private void AddVertexNormal(ObjModelPart workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Vertexs.Add(vertex);
        }

        private void SetWorkingMaterial(ObjModelPart workingObject, string value)
        {
            workingObject.MaterialName = value;
        }

        private void SetMaterialGroupName(ObjModel model, string value)
        {
            model.MaterialGroup = value.Substring(0, value.IndexOf(".mtl", StringComparison.Ordinal));
        }

        private ObjModelPart SetWorkingObject(ObjModel model, string name)
        {
            ObjModelPart part = new ObjModelPart();
            part.Name = name;
            model.Parts.Add(part);
            return part;
        }

        private float[] ParseFloatsFromString(string value)
        {
            string[] values = value.Split(new char[] { ' ', '/'});
            float[] result = new float[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                result[index] = float.Parse(values[index]);
            }
            return result;
        }
    }
}
