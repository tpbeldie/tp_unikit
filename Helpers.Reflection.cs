using System;

namespace tp_unikit.Helpers.Reflection
{
    public static class ReflectionHelper
    {
        public static T ParseEnum<T>(string value) where T : new() {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
