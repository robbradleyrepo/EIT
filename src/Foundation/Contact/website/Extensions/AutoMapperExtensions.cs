namespace LionTrust.Foundation.Contact.Extensions
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;

    public static class AutoMapperExtensions
    {
        public static object Map(this object @this, Type destinationType)
        {
            return Mapper.Map(@this, @this.GetType(), destinationType);
        }

        public static object Map(this IEnumerable<object> @this, Type destinationType)
        {
            return Mapper.Map(@this, @this.GetType(), destinationType);
        }
        /// <summary>
        /// Method map specific object into target type using automapper
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(this object @this)
        {
            if (@this == null)
            {
                return default(TDestination);
            }

            return (TDestination)@this.Map(typeof(TDestination));
        }
        public static TDestination Map<TDestination>(this object @this, TDestination destination)
        {
            return Mapper.Map(@this, destination);
        }
        public static TDestination Map<TSource, TDestination>(this TSource @this)
        {
            return Mapper.Map<TSource, TDestination>(@this);
        }
    }
}
