namespace HostAdapter
{
    public static class ReflectionCopier
    {
        public static TDestination Copy<TDestination>(object objectToClone) 
            where TDestination : new() 
        {
            var result = new TDestination();

            var properties = objectToClone.GetType().GetProperties();

            foreach (var sourceProperty in properties)
            {
                if (sourceProperty.CanRead)
                {
                    var destinationProperty = typeof (TDestination).GetProperty(sourceProperty.Name);
                    if (destinationProperty != null && destinationProperty.CanWrite)
                    {
                        var value = sourceProperty.GetValue(objectToClone);
                        destinationProperty.SetValue(result, value);
                    }
                }
            }

            return result;
        }
    }
}
