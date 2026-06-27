using System.Reflection;

namespace LegacyBarber.App.Util.Mapping
{
    public static class MapperUtil
    {
        public static TResult? Map<TSource, TResult>(TSource? source)
            where TSource : class
            where TResult : class, new()
        {
            TResult? result = default;
            return Map(source, result);
        }

        public static TResult? Map<TSource, TResult>(TSource? source, TResult? result)
            where TSource : class
            where TResult : class, new()
        {
            if (source != null)
            {
                result ??= new TResult();

                PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
                PropertyInfo[] resultProperties = typeof(TResult).GetProperties();
                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo? resultProperty = resultProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                    if (resultProperty != null && CheckTypeCompatibility(sourceProperty, resultProperty) && resultProperty.CanWrite)
                    {
                        object? value = sourceProperty.GetValue(source, null);
                        resultProperty.SetValue(result, value, null);
                    }
                }
            }
            return result;
        }

        private static bool CheckTypeCompatibility(PropertyInfo sourceProperty, PropertyInfo resultProperty)
        {
            if (resultProperty.PropertyType.IsGenericType
                && resultProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type? underlyingResultType = Nullable.GetUnderlyingType(resultProperty.PropertyType);
                return underlyingResultType != null && underlyingResultType.IsAssignableFrom(sourceProperty.PropertyType);
            }

            if (sourceProperty.PropertyType.IsGenericType
                && sourceProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type? underlyingSourceType = Nullable.GetUnderlyingType(sourceProperty.PropertyType);
                return underlyingSourceType != null && resultProperty.PropertyType.IsAssignableFrom(underlyingSourceType);
            }

            return resultProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType);
        }
    }
}
