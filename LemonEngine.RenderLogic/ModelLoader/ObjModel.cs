using System.Collections.Generic;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjModel
    {
        public string Name { get; set; }

        public string MaterialGroup { get; set; }

        public List<ObjModelPart> Parts = new List<ObjModelPart>();
    }
}