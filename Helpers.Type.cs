namespace tp_unikit.Helpers.Types
{
    public static class TypeHelper
    {
        public static T Assign<T>(ref T target, T value) {
            target = value;
            return value;
        }
    }
}
