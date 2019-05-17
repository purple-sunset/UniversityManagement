using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UniversityManagement.Utilities
{
    public class PagingParameter
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public PagingParameter()
        {
            Page = Constants.DefaultPage;
            PageSize = Constants.DefaultPageSize;
        }

        public PagingParameter(int page)
        {
            Page = page > 0 ? page : Constants.DefaultPage;
            PageSize = Constants.DefaultPageSize;
        }
        public PagingParameter(int page, int pageSize)
        {
            Page = page > 0 ? page : Constants.DefaultPage;
            PageSize = pageSize > 0 ? pageSize : Constants.DefaultPageSize;
        }
    }
    public class SortParameter<TEntity>
    {
        public string Property { get; set; }

        public bool Asc { get; set; }

        public SortParameter(string property, bool asc)
        {
            // Check TEntity has property or not
            // will throw exception on false
            var type = typeof(TEntity);
            type.GetProperty(property);

            Property = property;
            Asc = asc;
        }
    }

    public static class IQueryableHelper
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, List<SortParameter<TEntity>> sortParameters)
        {
            if (sortParameters != null && sortParameters.Any())
            {
                for(int i=0; i<sortParameters.Count; i++)
                {
                    var sortParam = sortParameters[i];
                    string command;
                    if(i == 0)
                    {
                        command = sortParam.Asc ? "OrderBy" : "OrderByDescending";
                    }
                    else
                    {
                        command = sortParam.Asc ? "ThenBy" : "ThenByDescending";
                    }
                    var property = sortParam.Property;
                    source = source.BuildExpression<TEntity>(command, property);
                }
            }
            return source;
        }

        public static IQueryable<TEntity> BuildExpression<TEntity>(this IQueryable<TEntity> source, string command, string member)
        {
            var type = typeof(TEntity);
            var property = type.GetProperty(member);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }

}
