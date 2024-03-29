﻿using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    internal class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(CodecoolContext db) : base(db)
        {
        }
    }
}