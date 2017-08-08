﻿namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelRepository
    {
        void StartLoad(IMaterialRepository materialRepo);
        IModel GetModelByName(string name);
    }
}
