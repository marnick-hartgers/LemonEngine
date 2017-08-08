namespace LemonEngine.Infrastructure.Types.Render
{
    public class Polygon
    {
        private Verticle[] _verticles;

        public Polygon(Verticle v1, Verticle v2, Verticle v3)
        {
            _verticles = new Verticle[3] { v1, v2, v3 };
        }

        public Verticle[] Verticles { get { return _verticles; } }



    }
}
