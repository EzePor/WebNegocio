using System.Reflection;

namespace WebNegocio.ExtensionMethod
{
    public static class ObjectExtensions
    {
        public static void CopyPropertiesTo<T>(this T source, T destination)
        {
            if (source == null || destination == null)
            {
                throw new ArgumentNullException("Objeto de origen o destino son nulos");
            }

            Type type = typeof(T);

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(source, null);
                    property.SetValue(destination, value, null);
                }
            }
        }
    }
}
