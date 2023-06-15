namespace tp_unikit.Helpers.Math
{
    public static class MathHelper
    {
        public static float SubtractPercent(this float input, int percent) {
            var extraction = percent / 100d * input;
            return input - (float)extraction;
        }
    }
}
