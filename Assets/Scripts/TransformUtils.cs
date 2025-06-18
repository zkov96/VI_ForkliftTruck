using UnityEngine;

public static class TransformUtils
{
    public static void Lerp(this Transform target, Transform a, Transform b, float t)
    {
        target.position = Vector3.Lerp(a.position, b.position, t);
        target.rotation = Quaternion.Lerp(a.rotation, b.rotation, t);
        target.localScale = Vector3.Lerp(a.localScale, b.localScale, t);
    }
}