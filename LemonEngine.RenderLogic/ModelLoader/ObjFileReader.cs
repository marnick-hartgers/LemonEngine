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

        public List<Model> ReadFolder(IMaterialRepository materialRepo)
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
            ObjModel model = new ObjModel();
            model.Name = Path.GetFileNameWithoutExtension(filename);
            string line = "";
            string currentName = "";
            ObjModelPart workingObject = null;
            IMaterialGroup workingMaterialGroup = null;
            IMaterial workingMaterial = null;
            while ((line = fileStream.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0 && line[0] != '#')
                {

                    string command = line.Substring(0, line.IndexOf(' '));
                    string value = line.Substring(line.IndexOf(' ') + 1);


                    switch (command.ToUpper())
                    {
                        case "O":
                            currentName = value;
                            break;
                        case "MTLLIB":
                            SetMaterialGroupName(out workingMaterialGroup, model, materialRepo, value);
                            break;
                        case "USEMTL":
                            SetWorkingMaterial(out workingMaterial, workingMaterialGroup, value);
                            workingObject = SetWorkingObject(model, $"{currentName}@{workingMaterial.Name}");
                            workingObject.Material = workingMaterial;
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
            return model.ToModel();
        }

        private void AddFace(ObjModelPart workingObject, IMaterial workingMaterial, string value)
        {
            var valueGroups = value.Split(' ');
            List<int> vertexes = new List<int>();
            List<int> normals = new List<int>();
            List<int> texCords = new List<int>();
            foreach (var valueGroup in valueGroups)
            {
                var valueByType = valueGroup.Split('/');
                vertexes.Add(ParseIntsFromString(valueByType[0]) - 1);
                if (valueByType.Length == 2)
                {
                    texCords.Add(ParseIntsFromString(valueByType[1]) - 1);
                }
                else if (valueByType.Length == 3)
                {
                    if (!string.IsNullOrEmpty(valueByType[1]))
                    {
                        texCords.Add(ParseIntsFromString(valueByType[1]) - 1);
                    }
                    if (!string.IsNullOrEmpty(valueByType[2]))
                    {
                        normals.Add(ParseIntsFromString(valueByType[2]) - 1);
                    }
                }
            }
            if (vertexes.Count == 3)
            {
                workingObject.FaceVertexes.AddRange(vertexes);
                workingObject.FaceNormals.AddRange(normals);
                workingObject.FaceTextCords.AddRange(texCords);
            }
            else if (vertexes.Count == 4)
            {
                //Triangulate
                workingObject.FaceVertexes.Add(vertexes[0]);
                workingObject.FaceVertexes.Add(vertexes[1]);
                workingObject.FaceVertexes.Add(vertexes[2]);

                workingObject.FaceVertexes.Add(vertexes[2]);
                workingObject.FaceVertexes.Add(vertexes[3]);
                workingObject.FaceVertexes.Add(vertexes[0]);

                workingObject.FaceNormals.Add(normals[0]);
                workingObject.FaceNormals.Add(normals[1]);
                workingObject.FaceNormals.Add(normals[2]);

                workingObject.FaceNormals.Add(normals[2]);
                workingObject.FaceNormals.Add(normals[3]);
                workingObject.FaceNormals.Add(normals[0]);

                if (texCords.Count == 4)
                {
                    workingObject.FaceTextCords.Add(texCords[0]);
                    workingObject.FaceTextCords.Add(texCords[1]);
                    workingObject.FaceTextCords.Add(texCords[2]);

                    workingObject.FaceTextCords.Add(texCords[2]);
                    workingObject.FaceTextCords.Add(texCords[3]);
                    workingObject.FaceTextCords.Add(texCords[0]);
                }


            }
            else
            {
                Console.WriteLine("OBJ vertext overflow: " + vertexes.Count + " in object " + workingObject.Name);
                for (int i = 0; i < vertexes.Count; i++)
                {
                    int m = vertexes.Count - 1;
                    workingObject.FaceVertexes.Add(vertexes[0 + i]);
                    workingObject.FaceVertexes.Add(vertexes[m % (1 + i)]);
                    workingObject.FaceVertexes.Add(vertexes[m % (2 + i)]);

                    workingObject.FaceNormals.Add(normals[0 + i]);
                    workingObject.FaceNormals.Add(normals[m % (1 + i)]);
                    workingObject.FaceNormals.Add(normals[m % (2 + i)]);
                    if (texCords.Count == vertexes.Count)
                    {
                        workingObject.FaceTextCords.Add(texCords[0 + i]);
                        workingObject.FaceTextCords.Add(texCords[m % (1 + i)]);
                        workingObject.FaceTextCords.Add(texCords[m % (2 + i)]);
                    }
                }
            }

        }

        private void AddVertex(ObjModel workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Vertexs.Add(vertex);
        }
        private void AddVertexTexture(ObjModel workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length == 2)
            {
                Vec3 vertex = new Vec3(values[0], values[1], 0);
                workingObject.TextCords.Add(vertex);
            }
            else if (values.Length == 3)
            {
                Vec3 vertex = new Vec3(values[0], values[1], values[2]);
                workingObject.TextCords.Add(vertex);
            }

        }
        private void AddVertexNormal(ObjModel workingObject, string value)
        {
            var values = ParseFloatsFromString(value);
            Vec3 vertex = new Vec3(values[0], values[1], values[2]);
            workingObject.Normals.Add(vertex);
        }

        private void SetWorkingMaterial(out IMaterial workingMaterial, IMaterialGroup materialGroup, string value)
        {
            workingMaterial = materialGroup.GetMaterialByName(value.Trim());
        }

        private void SetMaterialGroupName(out IMaterialGroup workingMaterialGroup, ObjModel model, IMaterialRepository materialRepo, string value)
        {
            string materialGroup = value.Substring(0, value.IndexOf(".mtl", StringComparison.Ordinal));
            model.MaterialGroup = materialGroup;
            workingMaterialGroup = materialRepo.GetMaterialGroup(materialGroup);
        }

        private ObjModelPart SetWorkingObject(ObjModel model, string name)
        {
            ObjModelPart part = new ObjModelPart();
            part.Name = $"{name}@";
            model.Parts.Add(part);
            return part;
        }

        private float[] ParseFloatsFromString(string value)
        {
            //value = value.Replace(".",",");
            string[] values = value.Trim().Split(new char[] { ' ', '/' });
            float[] result = new float[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index].IndexOf(".") != -1 && values[index].IndexOf(".") + 5 < values[index].Length)
                {
                    values[index] = values[index].Substring(0, values[index].IndexOf(".") + 6);
                }
                int exponent = 1;
                if (values[index].IndexOf('e') != -1)
                {
                    string exponentString = values[index].Substring(values[index].IndexOf('e'));
                    values[index] = values[index].Substring(0, values[index].IndexOf('e'));
                    if (exponentString != "e" && exponentString != "e-")
                    {
                        int ex = int.Parse(exponentString.Substring(1));
                        exponent = ex;
                    }

                }


                result[index] = float.Parse(values[index]);
                if (exponent != 1)
                {
                    result[index] = (float)Math.Pow(result[index], exponent);
                    Console.WriteLine("' " + values[index] + "'" + " = " + result[index] + "    exp:" + exponent);
                }

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
