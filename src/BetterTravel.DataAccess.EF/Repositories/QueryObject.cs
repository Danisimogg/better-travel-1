﻿using System;
using System.Linq.Expressions;

namespace BetterTravel.DataAccess.EF.Repositories
{
    public sealed class QueryObject<TEntity, TProjected>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
        public Expression<Func<TEntity, TProjected>> Projection { get; set; }
    }
    
    public sealed class QueryObject<TEntity>
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> WherePredicate { get; set; }
    }
}