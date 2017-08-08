using System;
using System.Collections.Generic;
using System.IO;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Model;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjFileReader
    {
        private string resourceFolder = "";

        public List<Model> ReadFolder( IMaterialRepository materialRepo)
        {
            List<Model> models = new List<Model>();
            MakeFolder();
            var filesToRead = GetFiles();
            foreach (var filename in filesToRead)
            {
                models.Add(ReadFile(filename, materialRepo));
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

        private Model ReadFile(string filename, IMaterialRepository materialRepo)
        {
            var fileStream = new StreamReader(filename);
            Model model = new Model();
            model.Name = Path.GetFileNameWithoutExtension(filename);
            string line = "";
            ModelPart workingObject = null;
            IMaterialGroup workingMaterialGroup = null;
            IMaterial workingMaterial = null;
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
                            SetMaterialGroupName(out workingMaterialGroup, model, materialRepo, value);

                            break;
                        case "USEMTL":
                            SetWorkingMaterial(out workingMaterial, workingMaterialGroup, value);
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
                            AddFace(workingObject, workingMaterial, value);
                            break;
                        case "#":
                            break;
                        default:
                            //ignore 
                            //Console.WriteLine("No command fount for: " + command + " on rule: " + line);
                            break;
                    }
                }
            }

            return model;
        }

        private void AddFace(ModelPart workingObject, IMaterial workingMaterial, string value)
        {
            ModelPartFace face = new ModelPartFace();
            face.Material = workingMaterial;
            var valueGroups = value.Split(' ');
            List<int> vertexes = new List<int>();
            List<int> texcords = new List<int>();
            List<int> normals  = new List<int>();
            foreach (var valueGroup in valueGroups)
            {
                var valueByType = valueGroup.Split('/');
                vertexes.Add(ParseIntsFromString(valueByType[0]) - 1);
                if (valueByType.Length == 2)
                {
                    texcords.Add(ParseIntsFromString(valueByType[1]) - 1);
                }else if (valueByType.Length == 3)
                {
                    if (!string.IsNullOrEmpty(valueByType[1]))
                    {
                        texcords.Add(ParseIntsFromString(valueByType[1]) - 1);
                    }
                    if (!string.IsNullOrEmpty(valueByType[2]))
                    {
                        normals.Add(ParseIntsFromString(valueByType[2]) - 1);
                    }
                }
            }
            face.Vertex = vertexes.ToArray();
            face.VertexTexture = texcords.ToArray();
            face.VertexNormal = normals.ToArray();
            face.HasVertexTexture = texcords.Count == vertexes.Count;
            face.HasVertexNormal = normals.Count == vertexes.Count;
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
                workingObject.VertexsTextures.Add(vertex);
            }
            else if (values.Length == 3)
            {
                Vec3 vertex = new Vec3(values[0], values[1], values[2]);
                workingObject.VertexsTextures.Add(vertex);
            }

        }
        private void AddVertexNormal(Model workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.VertexsNormal.Add(vertex);
        }

        private void SetWorkingMaterial(out IMaterial workingMaterial, IMaterialGroup materialGroup, string value)
        {
            workingMaterial = materialGroup.GetMaterialByName(value.Trim());
        }

        private void SetMaterialGroupName(out IMaterialGroup workingMaterialGroup, Model model, IMaterialRepository materialRepo, string value)
        {
            model.MaterialGroup = value.Substring(0, value.IndexOf(".mtl", StringComparison.Ordinal));
            workingMaterialGroup = materialRepo.GetMaterialGroup(model.MaterialGroup);
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
            //value = value.Replace(".",",");
            string[] values = value.Split(new char[] { ' ', '/'});
            float[] result = new float[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index].IndexOf(".") != -1 && values[index].IndexOf(".") + 5 < values[index].Length)
                {
                    values[index] = values[index].Substring(0, values[index].IndexOf(".") + 6); 
                }
                result[index] = float.Parse(values[index]);
                //Console.WriteLine(values[index] + "=" + result[index]);

            }
            return result;
        }
        private int ParseIntsFromString(string value)
        {
            var result = int.Parse(value);
            return result;
        }
    }
}
