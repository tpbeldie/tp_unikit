using System.Linq;

namespace tp_unikit.Helpers.Booleans
{
    public static class BooleanHelper
    {
        public static bool Check(params bool[] bools) {
            return bools.All(x => x);
        }
    }
}
