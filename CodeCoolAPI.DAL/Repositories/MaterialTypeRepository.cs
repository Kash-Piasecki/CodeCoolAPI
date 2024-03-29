﻿using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    internal class MaterialTypeRepository : Repository<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(CodecoolContext db) : base(db)
        {
        }
    }
}