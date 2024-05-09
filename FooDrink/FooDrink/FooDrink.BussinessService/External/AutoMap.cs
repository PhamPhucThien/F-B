namespace FooDrink.BussinessService.External
{
    public static class AutoMap<TSource, TDestination> where TSource : class where TDestination : class
    {
        public static TDestination Tranfers(TSource source)
        {
            TDestination destination = Activator.CreateInstance<TDestination>();
            System.Reflection.PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            System.Reflection.PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

            foreach (System.Reflection.PropertyInfo sourceProperty in sourceProperties)
            {
                foreach (System.Reflection.PropertyInfo destinationProperty in destinationProperties)
                {
                    if (sourceProperty.Name == destinationProperty.Name && sourceProperty.PropertyType == destinationProperty.PropertyType)
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
            return destination;
        }
    }

}
