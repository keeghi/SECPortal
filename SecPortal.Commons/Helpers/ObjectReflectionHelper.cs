namespace SecPortal.Commons.Helpers
{
    public static class ObjectReflectionHelper
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue<TValueType>(object src, string propName, TValueType value)
        {
            var configurationType = src.GetType();
            var targetField = configurationType.GetProperty(propName);
            if (targetField != null)
            {
                targetField.SetValue(src, value);
            }
            else
            {
                throw new System.Exception("Property not found");
            }
        }
    }
}
