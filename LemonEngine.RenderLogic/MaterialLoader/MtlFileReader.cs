using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Material;

namespace LemonEngine.RenderLogic.MaterialLoader
{
    public class MtlFileReader
    {
        private string resourceFolder = "";
        private Bitmap _emptyTexture;

        public MtlFileReader()
        {
            _emptyTexture = new Bitmap(1,1);
            _emptyTexture.SetPixel(0,0,Color.White);
        }

        public List<IMaterialGroup> ReadFolder()
        {
            List<IMaterialGroup> materialGroups = new List<IMaterialGroup>();
            MakeFolder();
            var filesToRead = GetFiles();
            foreach (var filename in filesToRead)
            {
                materialGroups.Add(ReadFile(filename));
            }
            return materialGroups;
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
                if (Path.GetExtension(fileInFolder).ToUpper() == ".MTL")
                {
                    filenames.Add(fileInFolder);
                }
            }

            return filenames;
        }

        private IMaterialGroup ReadFile(string filename)
        {
            List<IMaterial> materials = new List<IMaterial>();
            var fileStream = new StreamReader(filename);
            string line = "";
            MtlDefinition def = null;
            while ((line = fileStream.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {

                    int commandEnd = line.IndexOf(' ');
                    string command = line.Substring(0, commandEnd > 0 ? commandEnd : 1);
                    string value = line.Substring(line.IndexOf(' ') + 1);
                    switch (command.ToUpper())
                    {
                        case "NEWMTL":
                            if (def != null)
                            {
                                SaveDef(materials, def);
                            }
                            def = new MtlDefinition();
                            def.Name = value;
                            break;
                        case "KD":
                            SetDiffuse(def, value);
                            break;
                        case "MAP_KD":
                            SetTexture(def, value);
                            break;
                        case "KA":
                            SetAmbient(def, value);
                            break;
                        case "KS":
                            SetSpecular(def, value);
                            break;
                        default:
                            //ignore 
                            break;
                    }
                }
            }
            if (def != null)
            {
                SaveDef(materials, def);
            }
            MaterialGroup materialgroup = new MaterialGroup(Path.GetFileNameWithoutExtension(filename), materials.ToArray());
            return materialgroup;
        }

        private void SetTexture(MtlDefinition def, string value)
        {
            def.TextureLocation = value.Trim();
        }

        private void SetAmbient(MtlDefinition def, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length >= 3)
            {
                def.Ambient.X = values[0];
                def.Ambient.Y = values[1];
                def.Ambient.Z = values[2];
            }
        }

        private void SetDiffuse(MtlDefinition def, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length >= 3)
            {
                def.Diffuse.X = values[0];
                def.Diffuse.Y = values[1];
                def.Diffuse.Z = values[2];
            }
        }
        private void SetSpecular(MtlDefinition def, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length >= 3)
            {
                def.Specular.X = values[0];
                def.Specular.Y = values[1];
                def.Specular.Z = values[2];
            }
        }

        private void SetColor(MtlDefinition def, string value)
        {
            var values = ParseFloatsFromString(value);
            if (values.Length >= 3)
            {
                def.Color.X = values[0];
                def.Color.Y = values[1];
                def.Color.Z = values[2];
            }
        }

        private void SaveDef(List<IMaterial> materials, MtlDefinition def)
        {
            var m = new Material(def.Name);
            if (!string.IsNullOrEmpty(def.TextureLocation))
            {
                if (File.Exists(def.TextureLocation))
                {
                    m.Texture = GetBitmap(def.TextureLocation);
                    m.HasTexture = true;
                }
                else if (File.Exists(Path.Combine(resourceFolder, def.TextureLocation)))
                {
                    m.Texture = GetBitmap(Path.Combine(resourceFolder, def.TextureLocation));
                    m.HasTexture = true;
                }
            }
            else
            {
                //m.Texture = _emptyTexture;
                //m.HasTexture = true;
            }
            m.AmbColor = def.Ambient;
            m.Color = def.Diffuse;
            m.DifColor = def.Diffuse;
            m.SpeColor = def.Specular;

            materials.Add(m);
        }

        private Bitmap GetBitmap(string location)
        {
            return new Bitmap(File.OpenRead(location));
        }

        private float[] ParseFloatsFromString(string value)
        {
            //value = value.Replace(".", ",");
            string[] values = value.Split(new char[] { ' ', '/' });
            float[] result = new float[values.Length];
            for (int index = 0; index < values.Length; index++)
            {
                if (values[index].IndexOf(".") != -1 && values[index].IndexOf(".") + 5 < values[index].Length)
                {
                    values[index] = values[index].Substring(0, values[index].IndexOf(".") + 6);
                }
                result[index] = float.Parse(values[index]);
                if (result[index] > 1)
                {
                    
                }
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

    public class MtlDefinition
    {
        public string Name = "";
        public string TextureLocation = "";
        public Vec3 Color = new Vec3(1f,0f,0f);
        public Vec3 Ambient = new Vec3(1f, 0f, 0f);
        public Vec3 Diffuse = new Vec3(1f, 0f, 0f);
        public Vec3 Specular = new Vec3(1f, 0f, 0f);
        public float Illum = 7;
    }
}