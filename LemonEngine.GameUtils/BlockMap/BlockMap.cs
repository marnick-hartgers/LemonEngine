using LemonEngine.Infrastructure.Logic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.GameUtils.BlockMap
{
    public abstract class BlockMap<BlockTypeEnum>
    {
        private int _sizeX, _sizeY;
        protected int SizeX => _sizeX;
        protected int SizeY => _sizeY;
        protected BlockTypeEnum[,] _map;

        public BlockMap(int sizeX, int sizeY)
        {
            if (!typeof(BlockTypeEnum).IsEnum)
            {
                throw new InvalidOperationException("BlockTypeEnum must be a enum");
            }
            _sizeX = sizeX;
            _sizeY = sizeY;
            _map = new BlockTypeEnum[_sizeX, _sizeY];
        }

        public abstract IEntity GetBlockEntity(BlockTypeEnum blockType);
        
    }
}
