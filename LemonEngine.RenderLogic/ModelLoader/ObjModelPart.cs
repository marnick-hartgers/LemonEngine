using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Render.Renderable.Model;

namespace LemonEngine.RenderLogic.ModelLoader
{
    public class ObjModelPart
    {
        public ObjModelPart()
        {
            FaceVertexes = new List<int>();
            FaceNormals = new List<int>();
            FaceTextCords = new List<int>();
        }


        public string Name { get; set; }
        public IMaterial Material { get; set; }
        public List<int> FaceVertexes { get; }
        public List<int> FaceNormals { get; }
        public List<int> FaceTextCords { get; }
    }
}
