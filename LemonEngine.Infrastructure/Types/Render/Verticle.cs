namespace LemonEngine.Infrastructure.Types.Render
{
    public class Verticle
    {
        private Vec3 _normal, _position, _texCord;

        public Verticle()
        {
            _normal = new Vec3();
            _position = new Vec3();
            _texCord = new Vec3();
        }
        public Verticle(Vec3 normal, Vec3 position, Vec3 texturecoordinate)
        {
            _normal = normal;
            _position = position;
            _texCord = texturecoordinate;
        }

        public Verticle(float normalX, float normalY, float normalZ, float positionX, float positionY, float positionZ, float texturecoordinateX, float texturecoordinateY, float texturecoordinateZ)
        {
            _normal = new Vec3(normalX, normalY, normalZ);
            _position = new Vec3(positionX, positionY, positionZ);
            _texCord = new Vec3( texturecoordinateX, texturecoordinateY, texturecoordinateZ);
        }


    }
}
