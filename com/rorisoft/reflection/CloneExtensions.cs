using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace apicore9.com.rorisoft.reflection
{
    public class PropertyChange
    {
        public required string PropertyName { get; set; }
        public object? OriginalValue { get; set; }
        public object? NewValue { get; set; }
        public HashSet<string>? Decorators { get; set; }
    }

    public static class CloneExtensions
    {
        public static T? DeepClone<T>(this T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static List<PropertyChange> GetPropertyChanges<T>(this T original, T modified)
        {
            var changes = new List<PropertyChange>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var originalValue = property.GetValue(original);
                var modifiedValue = property.GetValue(modified);

                if (!AreEqual(originalValue, modifiedValue))
                {
                    changes.Add(new PropertyChange
                    {
                        PropertyName = property.Name,
                        OriginalValue = originalValue,
                        NewValue = modifiedValue,
                        Decorators = property.GetCustomAttributes(false).Select(x => x.GetType().Name).ToHashSet()
                    });
                }
            }

            return changes;
        }

        private static bool AreEqual(object? obj1, object? obj2)
        {
            try
            {
                if (obj1 is null && obj2 is null) return true;
                if (obj1 is null || obj2 is null) return false;

                if (obj1 is IEquatable<object> equatable)
                {
                    return equatable.Equals(obj2);
                }

                if (obj1.GetType().IsPrimitive || obj1 is string || obj1 is DateTime || obj1 is decimal)
                {
                    return obj1.Equals(obj2);
                }

                var json1 = JsonSerializer.Serialize(obj1);
                var json2 = JsonSerializer.Serialize(obj2);
                return json1 == json2;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
