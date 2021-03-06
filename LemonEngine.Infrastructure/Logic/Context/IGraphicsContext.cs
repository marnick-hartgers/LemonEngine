﻿using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public interface IGraphicsContext
    {
        void SetClearColor(Vec3 color);
    }
}
