using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

public static class ExtensionMethods
{
    public static void CopyPropertiesTo<T>(this T self, T copied)
    {
        PropertyInfo[] destinationProperties = copied.GetType().GetProperties();
        foreach (PropertyInfo destinationPi in destinationProperties)
        {
            PropertyInfo sourcePi = self.GetType().GetProperty(destinationPi.Name);
            destinationPi.SetValue(copied, sourcePi.GetValue(self, null), null);
        }
    }
}
