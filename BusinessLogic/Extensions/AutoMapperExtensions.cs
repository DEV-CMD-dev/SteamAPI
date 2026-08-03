using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreNull<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> map)
        {
            map.ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            {
                if (srcMember == null)
                    return false;
                if (srcMember is DateTime dt && dt == default)
                    return false;

                return true;
            }));

            return map;
        }
    }
}
