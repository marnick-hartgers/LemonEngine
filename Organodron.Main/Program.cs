using System;
using System.Threading.Tasks;
using Organodron.Main.Window;
using System.Windows.Forms;

namespace Organodron.Main
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Loading....");
            OrganodronMainWindow window = new OrganodronMainWindow();
            Console.WriteLine("Done");
            Application.Run(window);
        }
    }
}
