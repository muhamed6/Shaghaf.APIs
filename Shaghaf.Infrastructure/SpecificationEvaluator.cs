﻿using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Infrastructure
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery; 
            if (spec.Criteria is not null) 
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);


            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));



            return query;
        }
    }
}
