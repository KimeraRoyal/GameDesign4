using UnityEngine;

namespace Graph
{
    public static class ComparisonExtensions
    {
        public static bool AlmostEqual(this float a, float b)
            => Mathf.Abs(a - b) <= float.Epsilon * Mathf.Abs(a + b) * 2 ||
               Mathf.Abs(a - b) < float.MinValue;

        public static bool AlmostEqual(this Vector2 a, Vector2 b)
            => a.x.AlmostEqual(b.x) && a.y.AlmostEqual(b.y);

        public static bool AlmostEqual(this Vector3 a, Vector3 b)
            => a.x.AlmostEqual(b.x) && a.y.AlmostEqual(b.y) && a.z.AlmostEqual(b.z);

        public static bool AlmostEqual(this Vector4 a, Vector4 b)
            => a.x.AlmostEqual(b.x) && a.y.AlmostEqual(b.y) && a.z.AlmostEqual(b.z) && a.w.AlmostEqual(b.w);
    }
}